$containerName = "sqlserver2022"
$imageName = "sqlserver2022_backup"
$backupPath = "C:\SQL_DB\sqlserver2022_backup.tar"
docker commit $containerName $imageName
docker save -o $backupPath $imageName
