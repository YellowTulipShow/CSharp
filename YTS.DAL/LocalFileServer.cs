using System;
using YTS.Engine.LocalFile;
using YTS.Tools;

namespace YTS.DAL
{
    /// <summary>
    /// 本地文件存储服务-数据访问层(Data Access Layer)
    /// </summary>
    /// <typeparam name="M">数据映射模型</typeparam>
    public class LocalFileServer<M> :
        AbsDAL<M>,
        Model.File.IFileInfo
        where M : Model.File.AbsFile
    {
        /// <summary>
        /// 字段: 路径文件夹
        /// </summary>
        private readonly string _pathFolder_ = string.Empty;
        /// <summary>
        /// 字段: 文件名
        /// </summary>
        private readonly string _fileName_ = string.Empty;

        /// <summary>
        /// 列数据模型解析器
        /// </summary>
        public readonly FieldModelParser<M> modelParser = null;

        public LocalFileServer() : base() {
            this.modelParser = new FieldModelParser<M>();

            M model = ReflexHelp.CreateNewObject<M>();
            this._pathFolder_ = model.GetPathFolder();
            this._fileName_ = model.GetFileName();
        }

        #region ====== using:IFileInfo ======
        /// <summary>
        /// 获取 /(根目录) 相对路径文件夹 格式: /xxx/xxx
        /// </summary>
        /// <returns>相对路径</returns>
        public string GetPathFolder() {
            return this._pathFolder_;
        }

        /// <summary>
        /// 获取文件名称 (只是名称, 不需要后缀)
        /// </summary>
        /// <returns>文件名</returns>
        public string GetFileName() {
            return this._fileName_;
        }
        #endregion

        #region ====== using:IBasicDataAccess<M> ======
        public override bool Insert(M model) {
            throw new NotImplementedException();
        }

        public override bool Delete(string where) {
            throw new NotImplementedException();
        }

        public override bool Update(Model.KeyString[] keyvaluedic, string where) {
            throw new NotImplementedException();
        }

        public override M[] Select(int top = 0, string where = null, string sort = null) {
            throw new NotImplementedException();
        }

        public override M[] Select(int pageCount, int pageIndex, out int recordCount, string where = null, string sort = null) {
            throw new NotImplementedException();
        }

        public override int GetRecordCount(string where = null) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
