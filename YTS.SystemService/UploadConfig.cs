using System;
using YTS.Engine.ShineUpon;
using YTS.Tools;

namespace YTS.SystemService
{
    /// <summary>
    /// 上传配置
    /// </summary>
    public class UploadConfig : AbsConfig
    {
        public UploadConfig() : base() { }

        public override string GetFileName() {
            return @"UploadConfig.ini";
        }

        #region === Model Property ===
        /// <summary>
        /// 附件上传目录
        /// </summary>
        [Explain(@"附件上传目录")]
        [ShineUponProperty]
        public string File_Path { get { return _file_path; } set { _file_path = value; } }
        private string _file_path = string.Empty;


        /// <summary>
        /// 附件保存方式
        /// </summary>
        [Explain(@"附件保存方式")]
        [ShineUponProperty]
        public int File_Save { get { return _file_save; } set { _file_save = value; } }
        private int _file_save = 0;


        /// <summary>
        /// 允许匿名上传(0否1是)
        /// </summary>
        [Explain(@"允许匿名上传(0否1是)")]
        [ShineUponProperty]
        public int File_Anonymous { get { return _file_anonymous; } set { _file_anonymous = value; } }
        private int _file_anonymous = 0;


        /// <summary>
        /// 编辑器远程图片上传
        /// </summary>
        [Explain(@"编辑器远程图片上传")]
        [ShineUponProperty]
        public int File_Remote { get { return _file_remote; } set { _file_remote = value; } }
        private int _file_remote = 0;


        /// <summary>
        /// 附件上传类型
        /// </summary>
        [Explain(@"附件上传类型")]
        [ShineUponProperty]
        public string File_Extension { get { return _file_extension; } set { _file_extension = value; } }
        private string _file_extension = string.Empty;


        /// <summary>
        /// 视频上传类型
        /// </summary>
        [Explain(@"视频上传类型")]
        [ShineUponProperty]
        public string Video_Extension { get { return _video_extension; } set { _video_extension = value; } }
        private string _video_extension = string.Empty;


        /// <summary>
        /// 文件上传大小
        /// </summary>
        [Explain(@"文件上传大小")]
        [ShineUponProperty]
        public int File_Size { get { return _file_size; } set { _file_size = value; } }
        private int _file_size = 0;


        /// <summary>
        /// 视频上传大小
        /// </summary>
        [Explain(@"视频上传大小")]
        [ShineUponProperty]
        public int Videosize { get { return _videosize; } set { _videosize = value; } }
        private int _videosize = 0;


        /// <summary>
        /// 图片上传大小
        /// </summary>
        [Explain(@"图片上传大小")]
        [ShineUponProperty]
        public int Img_Size { get { return _img_size; } set { _img_size = value; } }
        private int _img_size = 0;


        /// <summary>
        /// 图片最大高度(像素)
        /// </summary>
        [Explain(@"图片最大高度(像素)")]
        [ShineUponProperty]
        public int Img_Max_Height { get { return _img_max_height; } set { _img_max_height = value; } }
        private int _img_max_height = 0;


        /// <summary>
        /// 图片最大宽度(像素)
        /// </summary>
        [Explain(@"图片最大宽度(像素)")]
        [ShineUponProperty]
        public int Img_Max_Width { get { return _img_max_width; } set { _img_max_width = value; } }
        private int _img_max_width = 0;


        /// <summary>
        /// 生成缩略图高度(像素)
        /// </summary>
        [Explain(@"生成缩略图高度(像素)")]
        [ShineUponProperty]
        public int Thumbnail_Height { get { return _thumbnail_height; } set { _thumbnail_height = value; } }
        private int _thumbnail_height = 0;


        /// <summary>
        /// 生成缩略图宽度(像素)
        /// </summary>
        [Explain(@"生成缩略图宽度(像素)")]
        [ShineUponProperty]
        public int Thumbnail_Width { get { return _thumbnail_width; } set { _thumbnail_width = value; } }
        private int _thumbnail_width = 0;


        /// <summary>
        /// 缩略图生成方式
        /// </summary>
        [Explain(@"缩略图生成方式")]
        [ShineUponProperty]
        public string Thumbnail_Mode { get { return _thumbnail_mode; } set { _thumbnail_mode = value; } }
        private string _thumbnail_mode = string.Empty;


        /// <summary>
        /// 图片水印类型
        /// </summary>
        [Explain(@"图片水印类型")]
        [ShineUponProperty]
        public int Watermark_Type { get { return _watermark_type; } set { _watermark_type = value; } }
        private int _watermark_type = 0;


        /// <summary>
        /// 图片水印位置
        /// </summary>
        [Explain(@"图片水印位置")]
        [ShineUponProperty]
        public int Watermark_Position { get { return _watermark_position; } set { _watermark_position = value; } }
        private int _watermark_position = 0;


        /// <summary>
        /// 图片生成质量
        /// </summary>
        [Explain(@"图片生成质量")]
        [ShineUponProperty]
        public int Watermark_Img_Quality { get { return _watermark_img_quality; } set { _watermark_img_quality = value; } }
        private int _watermark_img_quality = 0;


        /// <summary>
        /// 图片水印文件
        /// </summary>
        [Explain(@"图片水印文件")]
        [ShineUponProperty]
        public string Watermark_Pic { get { return _watermark_pic; } set { _watermark_pic = value; } }
        private string _watermark_pic = string.Empty;


        /// <summary>
        /// 水印透明度
        /// </summary>
        [Explain(@"水印透明度")]
        [ShineUponProperty]
        public int Watermark_Transparency { get { return _watermark_transparency; } set { _watermark_transparency = value; } }
        private int _watermark_transparency = 0;


        /// <summary>
        /// 水印文字
        /// </summary>
        [Explain(@"水印文字")]
        [ShineUponProperty]
        public string Watermark_Text { get { return _watermark_text; } set { _watermark_text = value; } }
        private string _watermark_text = string.Empty;


        /// <summary>
        /// 文字字体
        /// </summary>
        [Explain(@"文字字体")]
        [ShineUponProperty]
        public string Watermark_Font { get { return _watermark_font; } set { _watermark_font = value; } }
        private string _watermark_font = string.Empty;


        /// <summary>
        /// 文字大小(像素)
        /// </summary>
        [Explain(@"文字大小(像素)")]
        [ShineUponProperty]
        public int Watermark_Fontsize { get { return _watermark_fontsize; } set { _watermark_fontsize = value; } }
        private int _watermark_fontsize = 0;
        #endregion
    }
}