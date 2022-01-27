$urlDevOps = "https://dev.azure.com/joergkrause/Heise%20VideoCourse%20Support%20Repo/_apis/git/repositories/Heise%20VideoCourse%20Support%20Repo/items?scopePath=ARMTemplates/storage.json&api-version=6.0"

$url = "https://raw.githubusercontent.com/joergkrause/video-azurecloudnative/master/ARMTemplates/storage.json"
$urlEncoded = [uri]::EscapeDataString($url)

$targetUrl = "https://portal.azure.com/#create/Microsoft.Template/uri/$urlEncoded"

Write-Host($targetUrl)

