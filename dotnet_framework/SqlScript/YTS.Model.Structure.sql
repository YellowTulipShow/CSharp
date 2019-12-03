--Exec sp_defaultdb @loginame='login', @defdb='YTSDataB'
--Exec sp_defaultdb @loginame='login', @defdb='bds307921640_db'

/*
    删除数据表内容和表的三种方式:
    delete from TableName where id=1  -- 指定数据
    truncate table TableName -- 删除整个表数据,完全清空,但不删除格式结构
    drop table TableName -- 删除整个表,全部清空: 数据,格式,结构... 然后就没有然后了...
	
	查询所有表行数
		select b.name,a.row_count from sys.dm_db_partition_stats a,
		sys.objects b
		where a.object_id=b.object_id 
		and a.index_id<=1
		and b.type='U'
		order by a.row_count desc

	--创建表
	create table 表(a1 varchar(10),a2 char(2))
	--为表添加描述信息
	EXECUTE sp_addextendedproperty N'MS_Description', '人员信息表', N'user', N'dbo', N'table', N'表', NULL, NULL
	--为字段a1添加描述信息
	EXECUTE sp_addextendedproperty N'MS_Description', '姓名', N'user', N'dbo', N'table', N'表', N'column', N'a1'
	--为字段a2添加描述信息，
	EXECUTE sp_addextendedproperty N'MS_Description', '性别', N'user', N'dbo', N'table', N'表', N'column', N'a2'
	--更新表中列a1的描述属性：
	EXEC sp_updateextendedproperty 'MS_Description','字段1','user',dbo,'table','表','column',a1
	--删除表中列a1的描述属性：
	EXEC sp_dropextendedproperty 'MS_Description','user',dbo,'table','表','column',a1
	--删除测试
	drop table 表
	
	http://www.cnblogs.com/carekee/articles/2094676.html // SQL中char、varchar、nvarchar的区别
*/

/*
创建数据库====================================
    Create DataBase YTSSYS on (
	    Name ='YTSSYS',
	    FileName='D:\ZRQDownloads\DataBase\YTSSYS.mdf',
	    Size = 10MB,
	    MaxSize = UNLIMITED,
	    FileGrowth = 10%
    ) log on (
	    Name= 'YTSSYS_log',
	    FileName='D:\ZRQDownloads\DataBase\YTSSYS_log.ldf',
	    Size = 3MB, MaxSize = 5MB, FileGrowth = 1MB
    )
    sp_helpdb YTSSYS

修改数据库====================================
	--Alter DataBase YTSDataB
*/
