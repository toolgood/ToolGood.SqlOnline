using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.ReadyGo3.LinQ;
using ToolGood.ReadyGo3.PetaPoco;

namespace ToolGood.Infrastructure
{
    public interface IRepositoryObjectBase<T> where T : class, new()
    {
         /// <summary>
        /// 根据条件查询个数
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int Count(object condition = null);
        /// <summary>
        /// 根据条件查询个数，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<int> CountAsync(object condition = null);
 
        /// <summary>
        /// 根据条件从数据库中删除对象
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int Delete(object condition = null);
 
        /// <summary>
        /// 根据条件从数据库中删除对象 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(object condition = null);
 
        /// <summary>
        /// 根据条件判断是否存在
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        bool Exists(object condition = null);
        /// <summary>
        /// 根据条件是判断否存在，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(object condition = null);

        /// <summary>
        /// 根据条件查询第一个
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        T First(object condition = null);
        /// <summary>
        /// 根据条件查询第一个，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<T> FirstAsync(object condition = null);
        /// <summary>
        /// 根据条件查询第一个
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        T FirstOrDefault(object condition = null);
        /// <summary>
        /// 根据条件查询第一个，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(object condition = null);
 
        /// <summary>
        /// 根据条件查询页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Page<T> Page(long page, long itemsPerPage, object condition = null);
        /// <summary>
        ///  根据条件查询页，异步操作
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<Page<T>> PageAsync(long page, long itemsPerPage, object condition = null);
 
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        List<T> Select(long limit, long offset, object condition = null);
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        List<T> Select(long limit, object condition = null);
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        List<T> Select(object condition = null);
        /// <summary>
        /// 根据条件查询，异步操作
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<List<T>> SelectAsync(long limit, long offset, object condition = null);
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<List<T>> SelectAsync(long limit, object condition = null);
        /// <summary>
        /// 根据条件查询，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<List<T>> SelectAsync(object condition = null);
        /// <summary>
        /// 根据条件查询唯一项
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        T Single(object condition = null);
        /// <summary>
        /// 根据条件查询唯一项，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<T> SingleAsync(object condition = null);
 
        /// <summary>
        /// 根据条件查询唯一项
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        T SingleOrDefault(object condition = null);
        /// <summary>
        /// 根据条件查询唯一项，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<T> SingleOrDefaultAsync(object condition = null);
 

        /// <summary>
        /// 根据条件更新对象
        /// </summary>
        /// <param name="set"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        int Update(object set, object condition = null);
 

        /// <summary>
        /// 根据条件更新对象，异步操作
        /// </summary>
        /// <param name="set"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(object set, object condition = null);
 
    }


    public interface IRepositoryBase<T> where T : class, new()
    {
 
        /// <summary>
        /// 从数据库中删除对象
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        int Delete(T poco);
 
        /// <summary>
        /// 根据条件从数据库中删除对象，异步操作
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(T poco);
 
        /// <summary>
        /// 根据主键条件删除
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        int DeleteById(object primaryKey);
        /// <summary>
        /// 根据主键条件删除，异步操作
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<int> DeleteByIdAsync(object primaryKey);
 
        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        object Insert(T poco);
        /// <summary>
        /// 插入对象，异步操作
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        Task<object> InsertAsync(T poco);
        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="list"></param>
        void InsertList(List<T> list);
        /// <summary>
        /// 插入对象，异步操作
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task InsertListAsync(List<T> list);
 
        /// <summary>
        /// 保存，插入或更新
        /// </summary>
        /// <param name="poco"></param>
        void Save(T poco);
        /// <summary>
        /// 保存，插入或更新，异步操作
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        Task SaveAsync(T poco);
 
        /// <summary>
        /// 根据条件查询唯一项
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        T SingleById(object primaryKey);
        /// <summary>
        /// 根据条件查询唯一项，异步操作
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<T> SingleByIdAsync(object primaryKey);
 
        /// <summary>
        /// 根据条件查询唯一项
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        T SingleOrDefaultById(object primaryKey);
        /// <summary>
        /// 根据条件查询唯一项，异步操作
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<T> SingleOrDefaultByIdAsync(object primaryKey);

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        int Update(T poco);
 

        /// <summary>
        /// 根据条件更新对象，异步操作
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(T poco);
 
        /// <summary>
        /// 动态Where
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        WhereHelper<T> Where(Expression<Func<T, bool>> where);
        /// <summary>
        /// 动态Where
        /// </summary>
        /// <returns></returns>
        WhereHelper<T> Where();



        /// <summary>
        /// 使用事务
        /// </summary>
        /// <returns></returns>
        Transaction UseTransaction();
    }
}
