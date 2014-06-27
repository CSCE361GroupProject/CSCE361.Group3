Imports CSCE361.Group3.BizLogic
Imports System.Net
Imports System.Text
Imports System.IO

Module API_Imgur
    Private Const _clientID As String = "95bf259370b67b4"
    Private Const _clientSecret As String = "7c8b9605b4194a1a43b8025df9b7af2f14d81a65"

    'Credit: followed guide at http://pc-tips.net/imgur-api-vb-net/ on how to do this
    Public Function uploadImage(ByVal sImageByteString As String) As Results
        Dim oResults As New Results

        Dim webClient As New WebClient
        webClient.Headers.Add("Authorization", "Client-ID " & _clientID)
        Dim keys As New System.Collections.Specialized.NameValueCollection

        Try
            keys.Add("image", sImageByteString)
            Dim imgurResponseArr As Byte() = webClient.UploadValues("https://api.imgur.com/3/image", keys)
            Dim encodingResult = Encoding.ASCII.GetString(imgurResponseArr)
            Dim sRegex As New System.Text.RegularExpressions.Regex("link"":""(.*?)""")
            Dim sMatch As Match = sRegex.Match(encodingResult)
            Dim uploadedImageURL As String = sMatch.ToString.Replace("link"":""", "").Replace("""", "").Replace("\/", "/")
            oResults.sMessage = uploadedImageURL
            oResults.bSuccess = True
        Catch ex As Exception
            oResults.bSuccess = False
            'Upload fail - set return to empty string
            oResults.sMessage = ""
        End Try

        Return oResults
    End Function

End Module
