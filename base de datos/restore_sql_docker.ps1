$backupPath = "C:\SQL_DB\sqlserver2022_backup.tar"
$imageName = "sqlserver2022_backup"
$restoredContainerName = "sqlserver2022_restored"
docker load -i $backupPath
$existingContainer = docker ps -a --filter "name=$restoredContainerName" -q
if ($existingContainer) {
    docker rm -f $existingContainer | Out-Null
}
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=compraGamer432!" `
  -p 1433:1433 --name $restoredContainerName -d $imageName
pause