using System.Collections.Generic;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;

namespace ToolGood.Infrastructure
{
    public interface IUpdata
    {
        Dictionary<string, object> GetChanges();
    }

    public abstract class IUpdateBase : IUpdata
    {
        private Dictionary<string, object> _dictionary = new Dictionary<string, object>();

        protected void SetValue(string txt, object value) { _dictionary[txt] = value; }

        protected T GetValue<T>(string txt) { return _dictionary.ContainsKey(txt) ? (T)_dictionary[txt] : default(T); }

        Dictionary<string, object> IUpdata.GetChanges()
        {
            return _dictionary;
        }
    }

    public interface IRepositoryBase<T, T1>
        where T : class, new()
        where T1 : IUpdateBase, new()
    {
        /// <summary>
        /// 根据条件查询个数
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int Count(T1 condition);
        /// <summary>
        /// 根据条件查询个数，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<int> CountAsync(T1 condition);

        int Delete(T1 condition);

        /// <summary>
        /// 根据条件从数据库中删除对象 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(T1 condition);

        /// <summary>
        /// 根据条件判断是否存在
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        bool Exists(T1 condition);
        /// <summary>
        /// 根据条件是判断否存在，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(T1 condition);

        /// <summary>
        /// 根据条件查询第一个
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        T First(T1 condition = null);
        /// <summary>
        /// 根据条件查询第一个，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<T> FirstAsync(T1 condition);
        /// <summary>
        /// 根据条件查询第一个
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        T FirstOrDefault(T1 condition);
        /// <summary>
        /// 根据条件查询第一个，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(T1 condition);


        /// <summary>
        /// 根据条件查询页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Page<T> Page(long page, long itemsPerPage, T1 condition);
        /// <summary>
        ///  根据条件查询页，异步操作
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<Page<T>> PageAsync(long page, long itemsPerPage, T1 condition);

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        List<T> Select(long limit, long offset, T1 condition);
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        List<T> Select(long limit, T1 condition);
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        List<T> Select(T1 condition);
        /// <summary>
        /// 根据条件查询，异步操作
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<List<T>> SelectAsync(long limit, long offset, T1 condition);
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<List<T>> SelectAsync(long limit, T1 condition);
        /// <summary>
        /// 根据条件查询，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<List<T>> SelectAsync(T1 condition);
        /// <summary>
        /// 根据条件查询唯一项
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        T Single(T1 condition);
        /// <summary>
        /// 根据条件查询唯一项，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<T> SingleAsync(T1 condition);

        /// <summary>
        /// 根据条件查询唯一项
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        T SingleOrDefault(T1 condition);
        /// <summary>
        /// 根据条件查询唯一项，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<T> SingleOrDefaultAsync(T1 condition);

        /// <summary>
        /// 根据条件更新对象
        /// </summary>
        /// <param name="set"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        int Update(T1 set, T1 condition);


        /// <summary>
        /// 根据条件更新对象，异步操作
        /// </summary>
        /// <param name="set"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(T1 set, T1 condition);

    }


}
