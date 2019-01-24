using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using YTS.Tools;
using YTS.Tools.Const;

namespace YTS.Web.UI.Template
{
    #region 实体类组合===========================
    /// <summary>
    /// URL字典实体类
    /// </summary>
    [Serializable]
    public class UrlRewriteModel
    {
        //无参构造函数
        public UrlRewriteModel() { }

        private string _name = string.Empty;
        private string _type = string.Empty;
        private string _page = string.Empty;
        private string _inherit = string.Empty;
        private string _templet = string.Empty;
        private string _channel = string.Empty;
        private string _pagesize = string.Empty;
        private string _url_rewrite_str = string.Empty;
        private List<UrlRewrite_ItemModel> _url_rewrite_items;

        /// <summary>
        /// 名称标识
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 频道类型
        /// </summary>
        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// 源页面地址
        /// </summary>
        public string page
        {
            get { return _page; }
            set { _page = value; }
        }

        /// <summary>
        /// 页面继承的类名
        /// </summary>
        public string inherit
        {
            get { return _inherit; }
            set { _inherit = value; }
        }

        /// <summary>
        /// 模板文件名称
        /// </summary>
        public string templet
        {
            get { return _templet; }
            set { _templet = value; }
        }

        /// <summary>
        /// 所属频道名称
        /// </summary>
        public string channel
        {
            get { return _channel; }
            set { _channel = value; }
        }

        /// <summary>
        /// 每页数量，类型为列表页时启用该字段
        /// </summary>
        public string pagesize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }

        /// <summary>
        /// URL重写表达式连接字符串，后台编辑用到
        /// </summary>
        public string url_rewrite_str
        {
            get { return _url_rewrite_str; }
            set { _url_rewrite_str = value; }
        }

        /// <summary>
        /// URL字典重写表达式
        /// </summary>
        public List<UrlRewrite_ItemModel> url_rewrite_items
        {
            set { _url_rewrite_items = value; }
            get { return _url_rewrite_items; }
        }

    }
    
    /// <summary>
    /// URL字典重写表达式实体类
    /// </summary>
    [Serializable]
    public class UrlRewrite_ItemModel
    {
        //无参构造函数
        public UrlRewrite_ItemModel() { }

        private string _path = "";
        private string _pattern = "";
        private string _querystring = "";

        /// <summary>
        /// URL重写表达式
        /// </summary>
        public string path
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        /// <summary>
        /// 传输参数
        /// </summary>
        public string querystring
        {
            get { return _querystring; }
            set { _querystring = value; }
        }
    }
    #endregion

    #region 执行方法=============================
    public class UrlRewriteDAL
    {
        #region 基本方法==============================
        /// <summary>
        /// 增加节点
        /// </summary>
        public bool Add(UrlRewriteModel model)
        {
            try
            {
                string filePath = Utils.GetXmlMapPath(Names.FILE_URL_XML_CONFING);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode("urls");
                XmlElement xe = doc.CreateElement("rewrite");
                if (!string.IsNullOrEmpty(model.name))
                    xe.SetAttribute("name", model.name);
                if (!string.IsNullOrEmpty(model.type))
                    xe.SetAttribute("type", model.type);
                if (!string.IsNullOrEmpty(model.page))
                    xe.SetAttribute("page", model.page);
                if (!string.IsNullOrEmpty(model.inherit))
                    xe.SetAttribute("inherit", model.inherit);
                if (!string.IsNullOrEmpty(model.templet))
                    xe.SetAttribute("templet", model.templet);
                if (!string.IsNullOrEmpty(model.channel))
                    xe.SetAttribute("channel", model.channel);
                if (!string.IsNullOrEmpty(model.pagesize))
                    xe.SetAttribute("pagesize", model.pagesize);
                XmlNode newXn = xn.AppendChild(xe);

                //创建子节点
                foreach (UrlRewrite_ItemModel modelt in model.url_rewrite_items)
                {
                    XmlElement xeItem = doc.CreateElement("item");
                    if (!string.IsNullOrEmpty(modelt.path))
                        xeItem.SetAttribute("path", modelt.path);
                    if (!string.IsNullOrEmpty(modelt.pattern))
                        xeItem.SetAttribute("pattern", modelt.pattern);
                    if (!string.IsNullOrEmpty(modelt.querystring))
                        xeItem.SetAttribute("querystring", modelt.querystring);
                    newXn.AppendChild(xeItem);
                }

                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改节点
        /// </summary>
        public bool Edit(UrlRewriteModel model)
        {
            string filePath = Utils.GetXmlMapPath(Names.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                foreach (XmlElement xe in xnList)
                {
                    if (xe.Attributes["name"].Value.ToLower() == model.name.ToLower())
                    {
                        if (!string.IsNullOrEmpty(model.type))
                            xe.SetAttribute("type", model.type);
                        else if (xe.Attributes["type"] != null)
                            xe.Attributes["type"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.page))
                            xe.SetAttribute("page", model.page);
                        else if (xe.Attributes["page"] != null)
                            xe.Attributes["page"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.inherit))
                            xe.SetAttribute("inherit", model.inherit);
                        else if (xe.Attributes["inherit"] != null)
                            xe.Attributes["inherit"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.templet))
                            xe.SetAttribute("templet", model.templet);
                        else if (xe.Attributes["templet"] != null)
                            xe.Attributes["templet"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.channel))
                            xe.SetAttribute("channel", model.channel);
                        else if (xe.Attributes["channel"] != null)
                            xe.Attributes["channel"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.pagesize))
                            xe.SetAttribute("pagesize", model.pagesize);
                        else if (xe.Attributes["pagesize"] != null)
                            xe.Attributes["pagesize"].RemoveAll();

                        //移除所有的子节点重新添加
                        XmlNodeList itemXnList = xe.ChildNodes;
                        foreach (XmlElement itemXe in itemXnList)
                        {
                            for (int i = itemXnList.Count - 1; i >= 0; i--)
                            {
                                XmlElement xe2 = (XmlElement)itemXnList.Item(i);
                                xe.RemoveChild(xe2);
                            }
                        }
                        //创建子节点
                        foreach (UrlRewrite_ItemModel modelt in model.url_rewrite_items)
                        {
                            XmlElement xeItem = doc.CreateElement("item");
                            if (!string.IsNullOrEmpty(modelt.path))
                                xeItem.SetAttribute("path", modelt.path);
                            if (!string.IsNullOrEmpty(modelt.pattern))
                                xeItem.SetAttribute("pattern", modelt.pattern);
                            if (!string.IsNullOrEmpty(modelt.querystring))
                                xeItem.SetAttribute("querystring", modelt.querystring);
                            xe.AppendChild(xeItem);
                        }

                        doc.Save(filePath);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        public bool Remove(string attrName, string attrValue)
        {
            string filePath = Utils.GetXmlMapPath(Names.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                for (int i = xnList.Count - 1; i >= 0; i--)
                {
                    XmlElement xe = (XmlElement)xnList.Item(i);
                    if (xe.Attributes[attrName] != null && xe.Attributes[attrName].Value.ToLower() == attrValue.ToLower())
                    {
                        xn.RemoveChild(xe);
                    }
                }
                doc.Save(filePath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 批量删除节点
        /// </summary>
        public bool Remove(XmlNodeList xnList)
        {
            try
            {
                string filePath = Utils.GetXmlMapPath(Names.FILE_URL_XML_CONFING);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode("urls");
                foreach (XmlElement xe in xnList)
                {
                    for (int i = xn.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        XmlElement xe2 = (XmlElement)xn.ChildNodes.Item(i);
                        if (xe2.Attributes["name"].Value.ToLower() == xe.Attributes["name"].Value.ToLower())
                        {
                            xn.RemoveChild(xe2);
                        }
                    }
                }
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 导入节点
        /// </summary>
        public bool Import(XmlNodeList xnList)
        {
            try
            {
                string filePath = Utils.GetXmlMapPath(Names.FILE_URL_XML_CONFING);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode("urls");
                foreach (XmlElement xe in xnList)
                {
                    if (xe.NodeType != XmlNodeType.Comment && xe.Name.ToLower() == "rewrite")
                    {
                        if (xe.Attributes["name"] != null)
                        {
                            XmlNode n = doc.ImportNode(xe, true);
                            xn.AppendChild(n);
                        }
                    }
                }
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 扩展方法==============================
        /// <summary>
        /// 取得节点配制信息
        /// </summary>
        public UrlRewriteModel GetInfo(string attrValue)
        {
            UrlRewriteModel model = new UrlRewriteModel();
            string filePath = Utils.GetXmlMapPath(Names.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                foreach (XmlElement xe in xnList)
                {
                    if (xe.Attributes["name"].Value.ToLower() == attrValue.ToLower())
                    {
                        if (xe.Attributes["name"] != null)
                            model.name = xe.Attributes["name"].Value;
                        if (xe.Attributes["type"] != null)
                            model.type = xe.Attributes["type"].Value;
                        if (xe.Attributes["page"] != null)
                            model.page = xe.Attributes["page"].Value;
                        if (xe.Attributes["inherit"] != null)
                            model.inherit = xe.Attributes["inherit"].Value;
                        if (xe.Attributes["templet"] != null)
                            model.templet = xe.Attributes["templet"].Value;
                        if (xe.Attributes["channel"] != null)
                            model.channel = xe.Attributes["channel"].Value;
                        if (xe.Attributes["pagesize"] != null)
                            model.pagesize = xe.Attributes["pagesize"].Value;
                        //再次遍历子节点
                        List<UrlRewrite_ItemModel> lsItems = new List<UrlRewrite_ItemModel>();
                        foreach (XmlElement xe1 in xe.ChildNodes)
                        {
                            if (xe1.NodeType != XmlNodeType.Comment && xe1.Name.ToLower() == "item")
                            {
                                UrlRewrite_ItemModel item = new UrlRewrite_ItemModel();
                                if (xe1.Attributes["path"] != null)
                                    item.path = xe1.Attributes["path"].Value;
                                if (xe1.Attributes["pattern"] != null)
                                    item.pattern = xe1.Attributes["pattern"].Value;
                                if (xe1.Attributes["querystring"] != null)
                                    item.querystring = xe1.Attributes["querystring"].Value;
                                lsItems.Add(item);
                            }
                        }
                        model.url_rewrite_items = lsItems;
                        return model;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 根据请求的路径进行匹配配置文件当中是否存在此节点对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public UrlRewriteModel GetInfoModel_RequestPagePath(string path)
        {
            UrlRewriteModel model = new UrlRewriteModel();
            string filePath = Utils.GetXmlMapPath(Names.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                foreach (XmlElement xe in xnList)
                {
                    string panduan = "/aspx/YTSTemp/" + xe.Attributes["page"].Value.ToLower();
                    if (panduan.ToLower() == path.ToLower())
                    {
                        if (xe.Attributes["name"] != null)
                            model.name = xe.Attributes["name"].Value;
                        if (xe.Attributes["type"] != null)
                            model.type = xe.Attributes["type"].Value;
                        if (xe.Attributes["page"] != null)
                            model.page = xe.Attributes["page"].Value;
                        if (xe.Attributes["inherit"] != null)
                            model.inherit = xe.Attributes["inherit"].Value;
                        if (xe.Attributes["templet"] != null)
                            model.templet = xe.Attributes["templet"].Value;
                        if (xe.Attributes["channel"] != null)
                            model.channel = xe.Attributes["channel"].Value;
                        if (xe.Attributes["pagesize"] != null)
                            model.pagesize = xe.Attributes["pagesize"].Value;
                        //再次遍历子节点
                        List<UrlRewrite_ItemModel> lsItems = new List<UrlRewrite_ItemModel>();
                        foreach (XmlElement xe1 in xe.ChildNodes)
                        {
                            if (xe1.NodeType != XmlNodeType.Comment && xe1.Name.ToLower() == "item")
                            {
                                UrlRewrite_ItemModel item = new UrlRewrite_ItemModel();
                                if (xe1.Attributes["path"] != null)
                                    item.path = xe1.Attributes["path"].Value;
                                if (xe1.Attributes["pattern"] != null)
                                    item.pattern = xe1.Attributes["pattern"].Value;
                                if (xe1.Attributes["querystring"] != null)
                                    item.querystring = xe1.Attributes["querystring"].Value;
                                lsItems.Add(item);
                            }
                        }
                        model.url_rewrite_items = lsItems;
                        return model;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 取得URL配制列表
        /// </summary>
        public Hashtable GetList()
        {
            Hashtable ht = new Hashtable();
            List<UrlRewriteModel> ls = GetList("");
            foreach (UrlRewriteModel model in ls)
            {
                if (!ht.Contains(model.name))
                {
                    foreach (UrlRewrite_ItemModel item in model.url_rewrite_items)
                    {
                        item.querystring = item.querystring.Replace("^", "&");
                    }
                    ht.Add(model.name, model);
                }
            }
            return ht;
        }

        /// <summary>
        /// 取得URL配制列表
        /// </summary>
        public List<UrlRewriteModel> GetList(string channel)
        {
            List<UrlRewriteModel> ls = new List<UrlRewriteModel>();
            string filePath = Utils.GetXmlMapPath(Names.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            foreach (XmlElement xe in xn.ChildNodes)
            {
                if (xe.NodeType != XmlNodeType.Comment && xe.Name.ToLower() == "rewrite")
                {
                    if (xe.Attributes["name"] != null)
                    {
                        if (!string.IsNullOrEmpty(channel))
                        {
                            if (xe.Attributes["channel"] != null && channel.ToLower() == xe.Attributes["channel"].Value.ToLower())
                            {
                                UrlRewriteModel model = new UrlRewriteModel();
                                if (xe.Attributes["name"] != null)
                                    model.name = xe.Attributes["name"].Value;
                                if (xe.Attributes["type"] != null)
                                    model.type = xe.Attributes["type"].Value;
                                if (xe.Attributes["page"] != null)
                                    model.page = xe.Attributes["page"].Value;
                                if (xe.Attributes["inherit"] != null)
                                    model.inherit = xe.Attributes["inherit"].Value;
                                if (xe.Attributes["templet"] != null)
                                    model.templet = xe.Attributes["templet"].Value;
                                if (xe.Attributes["channel"] != null)
                                    model.channel = xe.Attributes["channel"].Value;
                                if (xe.Attributes["pagesize"] != null)
                                    model.pagesize = xe.Attributes["pagesize"].Value;
                                //再次遍历子节点
                                StringBuilder urlRewriteString = new StringBuilder();
                                List<UrlRewrite_ItemModel> lsItems = new List<UrlRewrite_ItemModel>();
                                foreach (XmlElement xe1 in xe.ChildNodes)
                                {
                                    if (xe1.NodeType != XmlNodeType.Comment && xe1.Name.ToLower() == "item")
                                    {
                                        UrlRewrite_ItemModel item = new UrlRewrite_ItemModel();
                                        if (xe1.Attributes["path"] != null)
                                            item.path = xe1.Attributes["path"].Value;
                                        if (xe1.Attributes["pattern"] != null)
                                            item.pattern = xe1.Attributes["pattern"].Value;
                                        if (xe1.Attributes["querystring"] != null)
                                            item.querystring = xe1.Attributes["querystring"].Value;
                                        urlRewriteString.Append(item.path + "," + item.pattern + "," + item.querystring + "&"); //组合成字符串
                                        lsItems.Add(item);
                                    }
                                }
                                model.url_rewrite_str = Utils.DelLastChar(urlRewriteString.ToString(), "&");
                                model.url_rewrite_items = lsItems;
                                ls.Add(model);
                            }
                        }
                        else
                        {
                            UrlRewriteModel model = new UrlRewriteModel();
                            if (xe.Attributes["name"] != null)
                                model.name = xe.Attributes["name"].Value;
                            if (xe.Attributes["type"] != null)
                                model.type = xe.Attributes["type"].Value;
                            if (xe.Attributes["page"] != null)
                                model.page = xe.Attributes["page"].Value;
                            if (xe.Attributes["inherit"] != null)
                                model.inherit = xe.Attributes["inherit"].Value;
                            if (xe.Attributes["templet"] != null)
                                model.templet = xe.Attributes["templet"].Value;
                            if (xe.Attributes["channel"] != null)
                                model.channel = xe.Attributes["channel"].Value;
                            if (xe.Attributes["pagesize"] != null)
                                model.pagesize = xe.Attributes["pagesize"].Value;
                            //再次遍历子节点
                            StringBuilder urlRewriteString = new StringBuilder();
                            List<UrlRewrite_ItemModel> lsItems = new List<UrlRewrite_ItemModel>();
                            foreach (XmlElement xe1 in xe.ChildNodes)
                            {
                                if (xe1.NodeType != XmlNodeType.Comment && xe1.Name.ToLower() == "item")
                                {
                                    UrlRewrite_ItemModel item = new UrlRewrite_ItemModel();
                                    if (xe1.Attributes["path"] != null)
                                        item.path = xe1.Attributes["path"].Value;
                                    if (xe1.Attributes["pattern"] != null)
                                        item.pattern = xe1.Attributes["pattern"].Value;
                                    if (xe1.Attributes["querystring"] != null)
                                        item.querystring = xe1.Attributes["querystring"].Value;
                                    urlRewriteString.Append(item.path + "," + item.pattern + "," + item.querystring + "&"); //组合成字符串
                                    lsItems.Add(item);
                                }
                            }
                            model.url_rewrite_str = Utils.DelLastChar(urlRewriteString.ToString(), "&");
                            model.url_rewrite_items = lsItems;
                            ls.Add(model);
                        }
                    }
                }
            }
            return ls;
        }

        #endregion
    }
    #endregion
}
