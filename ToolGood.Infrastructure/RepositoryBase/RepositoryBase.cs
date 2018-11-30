using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.ReadyGo3.LinQ;
using ToolGood.ReadyGo3.PetaPoco;

namespace ToolGood.Infrastructure
{
    public abstract partial class RepositoryBase<T> : IRepositoryObjectBase<T>, IRepositoryBase<T> where T : class, new()
    {
        protected SqlHelper _sqlHelper;
        protected virtual SqlHelper CreateSqlHelper()
        {
            if (_sqlHelper == null) {
                _sqlHelper = Config.DbHelper;
            }
            return _sqlHelper;
        }


        /// <summary>
        /// 根据条件查询个数
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int Count(object condition)
        {
            return CreateSqlHelper().Count<T>(ConditionObjectToWhere(condition));
        }
        /// <summary>
        /// 根据条件查询个数，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<int> CountAsync(object condition)
        {
            return CreateSqlHelper().CountAsync<T>(ConditionObjectToWhere(condition));

        }
        /// <summary>
        /// 从数据库中删除对象
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        public int Delete(T poco)
        {
            return CreateSqlHelper().Delete(poco);
        }
        /// <summary>
        /// 根据条件从数据库中删除对象
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int Delete(object condition)
        {
            return CreateSqlHelper().Delete<T>(ConditionObjectToWhere(condition));

        }
        /// <summary>
        /// 根据条件从数据库中删除对象，异步操作
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        public Task<int> DeleteAsync(T poco)
        {
            return CreateSqlHelper().DeleteAsync<T>(poco);

        }
        /// <summary>
        /// 根据条件从数据库中删除对象 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<int> DeleteAsync(object condition)
        {
            return CreateSqlHelper().DeleteAsync<T>(ConditionObjectToWhere(condition));

        }
        /// <summary>
        /// 根据主键条件删除
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public int DeleteById(object primaryKey)
        {
            return CreateSqlHelper().DeleteById<T>(primaryKey);
        }
        /// <summary>
        /// 根据主键条件删除，异步操作
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public Task<int> DeleteByIdAsync(object primaryKey)
        {
            return CreateSqlHelper().DeleteByIdAsync<T>(primaryKey);
        }
        /// <summary>
        /// 根据条件判断是否存在
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool Exists(object condition)
        {
            return CreateSqlHelper().Exists<T>(ConditionObjectToWhere(condition));

        }
        /// <summary>
        /// 根据条件是判断否存在，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<bool> ExistsAsync(object condition)
        {
            return CreateSqlHelper().ExistsAsync<T>(ConditionObjectToWhere(condition));

        }

        /// <summary>
        /// 根据条件查询第一个
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public T First(object condition)
        {
            return CreateSqlHelper().First<T>(ConditionObjectToWhere(condition));

        }
        /// <summary>
        /// 根据条件查询第一个，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<T> FirstAsync(object condition)
        {
            return CreateSqlHelper().FirstAsync<T>(ConditionObjectToWhere(condition));

        }
        /// <summary>
        /// 根据条件查询第一个
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public T FirstOrDefault(object condition)
        {
            return CreateSqlHelper().FirstOrDefault<T>(ConditionObjectToWhere(condition));

        }
        /// <summary>
        /// 根据条件查询第一个，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<T> FirstOrDefaultAsync(object condition)
        {
            return CreateSqlHelper().FirstOrDefaultAsync<T>(ConditionObjectToWhere(condition));
        }
        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        public object Insert(T poco)
        {
            return CreateSqlHelper().Insert(poco);
        }
        /// <summary>
        /// 插入对象，异步操作
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        public Task<object> InsertAsync(T poco)
        {
            return CreateSqlHelper().InsertAsync(poco);
        }
        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="list"></param>
        public void InsertList(List<T> list)
        {
            CreateSqlHelper().InsertList(list);
        }
        /// <summary>
        /// 插入对象，异步操作
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Task InsertListAsync(List<T> list)
        {
            return CreateSqlHelper().InsertListAsync(list);
        }
        /// <summary>
        /// 根据条件查询页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Page<T> Page(long page, long itemsPerPage, object condition)
        {
            return CreateSqlHelper().Page<T>(page, itemsPerPage, ConditionObjectToWhere(condition));
        }
        /// <summary>
        ///  根据条件查询页，异步操作
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<Page<T>> PageAsync(long page, long itemsPerPage, object condition)
        {
            return CreateSqlHelper().PageAsync<T>(page, itemsPerPage, ConditionObjectToWhere(condition));
        }
        /// <summary>
        /// 保存，插入或更新
        /// </summary>
        /// <param name="poco"></param>
        public void Save(T poco)
        {
            CreateSqlHelper().Save(poco);
        }
        /// <summary>
        /// 保存，插入或更新，异步操作
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        public Task SaveAsync(T poco)
        {
            return CreateSqlHelper().SaveAsync(poco);
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<T> Select(long limit, long offset, object condition)
        {
            return CreateSqlHelper().Select<T>(limit, offset, ConditionObjectToWhere(condition));
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<T> Select(long limit, object condition)
        {
            return CreateSqlHelper().Select<T>(limit, ConditionObjectToWhere(condition));
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<T> Select(object condition)
        {
            return CreateSqlHelper().Select<T>(ConditionObjectToWhere(condition));
        }
        /// <summary>
        /// 根据条件查询，异步操作
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<List<T>> SelectAsync(long limit, long offset, object condition)
        {
            return CreateSqlHelper().SelectAsync<T>(limit, offset, ConditionObjectToWhere(condition));
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<List<T>> SelectAsync(long limit, object condition)
        {
            return CreateSqlHelper().SelectAsync<T>(limit, ConditionObjectToWhere(condition));

        }
        /// <summary>
        /// 根据条件查询，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<List<T>> SelectAsync(object condition)
        {
            return CreateSqlHelper().SelectAsync<T>(ConditionObjectToWhere(condition));
        }
        /// <summary>
        /// 根据条件查询唯一项
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public T Single(object condition)
        {
            return CreateSqlHelper().Single<T>(ConditionObjectToWhere(condition));
        }

        /// <summary>
        /// 根据条件查询唯一项，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<T> SingleAsync(object condition)
        {
            return CreateSqlHelper().SingleAsync<T>(ConditionObjectToWhere(condition));
        }
        /// <summary>
        /// 根据条件查询唯一项
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public T SingleById(object primaryKey)
        {
            return CreateSqlHelper().SingleById<T>(primaryKey);
        }
        /// <summary>
        /// 根据条件查询唯一项，异步操作
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public Task<T> SingleByIdAsync(object primaryKey)
        {
            return CreateSqlHelper().SingleByIdAsync<T>(primaryKey);
        }
        /// <summary>
        /// 根据条件查询唯一项
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public T SingleOrDefault(object condition)
        {
            return CreateSqlHelper().SingleOrDefault<T>(ConditionObjectToWhere(condition));
        }
        /// <summary>
        /// 根据条件查询唯一项，异步操作
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<T> SingleOrDefaultAsync(object condition)
        {
            return CreateSqlHelper().SingleOrDefaultAsync<T>(ConditionObjectToWhere(condition));
        }
        /// <summary>
        /// 根据条件查询唯一项
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public T SingleOrDefaultById(object primaryKey)
        {
            return CreateSqlHelper().SingleOrDefaultById<T>(primaryKey);
        }
        /// <summary>
        /// 根据条件查询唯一项，异步操作
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public Task<T> SingleOrDefaultByIdAsync(object primaryKey)
        {
            return CreateSqlHelper().SingleOrDefaultByIdAsync<T>(primaryKey);
        }
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        public int Update(T poco)
        {
            return CreateSqlHelper().Update(poco);
        }
        /// <summary>
        /// 根据条件更新对象
        /// </summary>
        /// <param name="set"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int Update(object set, object condition)
        {
            return CreateSqlHelper().Update<T>(ConditionObjectToUpdateSetWhere(set, condition));
        }
        /// <summary>
        /// 根据条件更新对象，异步操作
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        public Task<int> UpdateAsync(T poco)
        {
            return CreateSqlHelper().UpdateAsync(poco);
        }
        /// <summary>
        /// 根据条件更新对象，异步操作
        /// </summary>
        /// <param name="set"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Task<int> UpdateAsync(object set, object condition)
        {
            return CreateSqlHelper().UpdateAsync<T>(ConditionObjectToUpdateSetWhere(set, condition));
        }
        /// <summary>
        /// 动态Where
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public WhereHelper<T> Where(Expression<Func<T, bool>> where)
        {
            return CreateSqlHelper().Where<T>(where);
        }
        /// <summary>
        /// 动态Where
        /// </summary>
        /// <returns></returns>
        public WhereHelper<T> Where()
        {
            return CreateSqlHelper().Where<T>();
        }

        /// <summary>
        /// 使用事务
        /// </summary>
        /// <returns></returns>
        public Transaction UseTransaction()
        {
            return CreateSqlHelper().UseTransaction();
        }


        private string ConditionObjectToWhere(object condition)
        {
            if (condition == null) {
                return "";
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("WHERE ");
            ObjectToSql(stringBuilder, condition, " AND ");
            return stringBuilder.ToString();
        }

        private string ConditionObjectToUpdateSetWhere(object set, object condition)
        {
            if (set == null) {
                throw new ArgumentException("set is  null object!");
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SET ");
            ObjectToSql(stringBuilder, set, ",");
            if (condition != null) {
                stringBuilder.Append(" WHERE ");
                ObjectToSql(stringBuilder, condition, " AND ");
            }
            return stringBuilder.ToString();
        }

        private void ObjectToSql(StringBuilder stringBuilder, object condition, string middelStr)
        {
            if (condition is IEnumerable) { throw new ArgumentException("condition is IEnumerable object!"); }

            var type = condition.GetType();
            var pis = type.GetProperties();
            for (int i = 0; i < pis.Length; i++) {
                if (i > 0) { stringBuilder.Append(middelStr); }
                var pi = pis[i];
                stringBuilder.Append($"[{pi.Name}]=");
                var pt = pi.PropertyType;
                var value = pi.GetMethod.Invoke(condition, null);
                stringBuilder.Append(EscapeParam(value));
            }
        }
        protected string EscapeParam(object value)
        {
            if (object.Equals(value, null)) return "NULL";

            var fieldType = value.GetType();
            if (fieldType.IsEnum) {
                var isEnumFlags = fieldType.IsEnum;
                if (!isEnumFlags && Int64.TryParse(value.ToString(), out long enumValue)) {
                    value = Enum.ToObject(fieldType, enumValue).ToString();
                }
                var enumString = value.ToString();
                return !isEnumFlags ? "'" + enumString.Trim('"') + "'" : enumString;
            }

            var typeCode = Type.GetTypeCode(fieldType);
            switch (typeCode) {
                case TypeCode.Boolean: return (bool)value ? "1" : "0";
                case TypeCode.Single: return ((float)value).ToString(CultureInfo.InvariantCulture);
                case TypeCode.Double: return ((double)value).ToString(CultureInfo.InvariantCulture);
                case TypeCode.Decimal: return ((decimal)value).ToString(CultureInfo.InvariantCulture);
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64: return value.ToString();
                default: break;
            }
            if (value is string || value is char) {
                var txt = (value.ToString()).Replace(@"\", @"\\").Replace("'", @"\'");
                return "'" + txt + "'";
            }
            if (fieldType == typeof(DateTime)) return "'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            if (fieldType == typeof(TimeSpan)) return ((TimeSpan)value).Ticks.ToString(CultureInfo.InvariantCulture);
            if (fieldType == typeof(byte[])) {
                var txt = Encoding.BigEndianUnicode.GetString((byte[])value);
                return "X'" + txt + "'";
            }
            return "'" + value.ToString() + "'";
        }

    }

    public abstract partial class RepositoryBase<T, T1> : RepositoryBase<T>,
        IRepositoryObjectBase<T>,
        IRepositoryBase<T>,
        IRepositoryBase<T, T1>
        where T : class, new()
        where T1 : IUpdateBase, new()
    {
        public int Count(T1 condition)
        {
            return CreateSqlHelper().Count<T>(ConditionObjectToWhere(condition));
        }

        public Task<int> CountAsync(T1 condition)
        {
            return CreateSqlHelper().CountAsync<T>(ConditionObjectToWhere(condition));
        }

        public int Delete(T1 condition)
        {
            return CreateSqlHelper().Delete<T>(ConditionObjectToWhere(condition));
        }

        public Task<int> DeleteAsync(T1 condition)
        {
            return CreateSqlHelper().DeleteAsync<T>(ConditionObjectToWhere(condition));
        }

        public bool Exists(T1 condition)
        {
            return CreateSqlHelper().Exists<T>(ConditionObjectToWhere(condition));
        }

        public Task<bool> ExistsAsync(T1 condition)
        {
            return CreateSqlHelper().ExistsAsync<T>(ConditionObjectToWhere(condition));
        }

        public T First(T1 condition)
        {
            return CreateSqlHelper().First<T>(ConditionObjectToWhere(condition));
        }

        public Task<T> FirstAsync(T1 condition)
        {
            return CreateSqlHelper().FirstAsync<T>(ConditionObjectToWhere(condition));
        }

        public T FirstOrDefault(T1 condition)
        {
            return CreateSqlHelper().FirstOrDefault<T>(ConditionObjectToWhere(condition));
        }

        public Task<T> FirstOrDefaultAsync(T1 condition)
        {
            return CreateSqlHelper().FirstOrDefaultAsync<T>(ConditionObjectToWhere(condition));
        }

        public Page<T> Page(long page, long itemsPerPage, T1 condition)
        {
            return CreateSqlHelper().Page<T>(page, itemsPerPage, ConditionObjectToWhere(condition));
        }

        public Task<Page<T>> PageAsync(long page, long itemsPerPage, T1 condition)
        {
            return CreateSqlHelper().PageAsync<T>(page, itemsPerPage, ConditionObjectToWhere(condition));
        }

        public List<T> Select(long limit, long offset, T1 condition)
        {
            return CreateSqlHelper().Select<T>(limit, offset, ConditionObjectToWhere(condition));
        }

        public List<T> Select(long limit, T1 condition)
        {
            return CreateSqlHelper().Select<T>(limit, ConditionObjectToWhere(condition));
        }

        public List<T> Select(T1 condition)
        {
            return CreateSqlHelper().Select<T>(ConditionObjectToWhere(condition));
        }

        public Task<List<T>> SelectAsync(long limit, long offset, T1 condition)
        {
            return CreateSqlHelper().SelectAsync<T>(limit, offset, ConditionObjectToWhere(condition));
        }

        public Task<List<T>> SelectAsync(long limit, T1 condition)
        {
            return CreateSqlHelper().SelectAsync<T>(limit, ConditionObjectToWhere(condition));
        }

        public Task<List<T>> SelectAsync(T1 condition)
        {
            return CreateSqlHelper().SelectAsync<T>(ConditionObjectToWhere(condition));
        }

        public T Single(T1 condition)
        {
            return CreateSqlHelper().Single<T>(ConditionObjectToWhere(condition));
        }

        public Task<T> SingleAsync(T1 condition)
        {
            return CreateSqlHelper().SingleAsync<T>(ConditionObjectToWhere(condition));
        }

        public T SingleOrDefault(T1 condition)
        {
            return CreateSqlHelper().SingleOrDefault<T>(ConditionObjectToWhere(condition));

        }

        public Task<T> SingleOrDefaultAsync(T1 condition)
        {
            return CreateSqlHelper().SingleOrDefaultAsync<T>(ConditionObjectToWhere(condition));
        }

        public int Update(T1 set, T1 condition)
        {
            return CreateSqlHelper().Update<T>(ConditionObjectToUpdateSetWhere(set, condition));
        }

        public Task<int> UpdateAsync(T1 set, T1 condition)
        {
            return CreateSqlHelper().UpdateAsync<T>(ConditionObjectToUpdateSetWhere(set, condition));
        }

        private string ConditionObjectToWhere(T1 condition)
        {
            if (condition == null) {
                return "";
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("WHERE ");
            ObjectToSql(stringBuilder, condition, " AND ");
            return stringBuilder.ToString();
        }

        private string ConditionObjectToUpdateSetWhere(T1 set, T1 condition)
        {
            if (set == null) {
                throw new ArgumentException("set is  null object!");
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SET ");
            ObjectToSql(stringBuilder, set, ",");
            if (condition != null) {
                stringBuilder.Append(" WHERE ");
                ObjectToSql(stringBuilder, condition, " AND ");
            }
            return stringBuilder.ToString();
        }

        private void ObjectToSql(StringBuilder stringBuilder, T1 condition, string middelStr)
        {
            var dict = ((IUpdata)condition).GetChanges();
            int index = 0;
            foreach (var item in dict) {
                if (index > 0) { stringBuilder.Append(middelStr); }
                index++;
                stringBuilder.Append($"[{item.Key}]={EscapeParam(item.Value)}");
            }
        }
    }

}
