# 使用说明

1. "Default"（默认值）
	1. "Monitor"
		1. "Path": 监控目录
		2. "FileNameKey":指定 '_'
		3. "FileExtensions":固定 [ "png", "bmp", "jpg" ]
	2. "Operation"
		1. "NetAddress": SimpleEye软件图像数据的监听地址
		2. "MovePath": 图像移动的目标目录
		3. "BackupPath":图像备份地址
		4. "Ftp"
			1. "FtpRoot": 格式要正确
			2. "UserName": 用户名
			3. "Password": 密码
			4. "Timeout": 发送超时时间
2. "Groups":针对每一种图像文件类型配置方案
	1. "Name": 在文件需要备份的情况下起效果，它是备份地址的子目录
	2. "Monitor"
		1. "FileNameKey":文件名开头
		2. "Operations":各种操作组合
			1. EnableNetTransfer NetAddress
			2. EnableFtpTransfer Ftp
			3. EnableMove MovePath
			4. EnableBackup BackupPath
			5. EnableDelete
			6. DisableFileNameAddedTimeStamp
			7. InProcessTransferKey: 当Scanner如果集成在其它软件中，配置为指定的字符串。功能与NetTransfer相同，但无网络开销。SimpleEye的字符串为'imagedto'