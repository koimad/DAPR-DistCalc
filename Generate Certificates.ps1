$password = ConvertTo-SecureString 'Pass@word10' -AsPlainText -Force

$certKeyPath = $env:USERPROFILE +"\source\repos\DAPR-DistCalc\Certificates\subtractServer.pfx"
$encodedBytesPath = $env:USERPROFILE +"\source\repos\DAPR-DistCalc\Certificates\subtractServerEncodedBytes.txt"

$cert = New-SelfSignedCertificate -DnsName @("localhost", "subtractserver") -CertStoreLocation "cert:\LocalMachine\My"
#$cert = New-SelfSignedCertificate -DnsName @("localhost", "addserver") -CertStoreLocation "cert:\CurrentUser\My"

$cert | Export-PfxCertificate -FilePath $certKeyPath -Password $password

$fileContentBytes = get-content $certKeyPath -Encoding Byte

[System.Convert]::ToBase64String($fileContentBytes) | Out-File $encodedBytesPath

$cert | Remove-Item