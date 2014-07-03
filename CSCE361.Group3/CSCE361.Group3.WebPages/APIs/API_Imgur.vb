﻿Imports CSCE361.Group3.BizLogic
Imports ExifLib
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

Module API_ExifLib


    'Reference: http://www.codeproject.com/Articles/36342/ExifLib-A-Fast-Exif-Data-Extractor-for-NET?msg=4682284#xx4682284xx 
    'returns a 2-dimensional array with latitude and longitude in double/decimal
    'needs testing
    Function getGeoData(ByVal originalFilePath As String) As Double()

        Dim fileLocation As String = originalFilePath
        Dim reader As ExifReader = New ExifReader(fileLocation)

        Dim latitudeDMS() As Double
        Dim longitudeDMS() As Double
        Dim latitudeRef As String
        Dim longitudeRef As String

        reader.GetTagValue(ExifTags.GPSLatitude, latitudeDMS)
        reader.GetTagValue(ExifTags.GPSLongitude, longitudeDMS)
        reader.GetTagValue(ExifTags.GPSLatitudeRef, latitudeRef)
        reader.GetTagValue(ExifTags.GPSLongitudeRef, longitudeRef)

        Dim geoData() As Double

        'todo: losing some accuracy on the conversion and it is causing misplacemnt
        'look into using a long instead of double if possible
        If longitudeRef Is Nothing Then
            geoData = {Nothing, Nothing}
        Else
            Dim latitudeDeg As Double = latitudeDMS(0)
            Dim latitudeMin As Double = latitudeDMS(1)
            Dim latitudeSec As Double = latitudeDMS(2)
            Dim latitudeDouble As Double
            If String.Compare("S", latitudeRef) Then
                latitudeDouble = (latitudeDeg + latitudeMin / 60 + latitudeDeg / 3600)
            Else
                latitudeDouble = -1 * (latitudeDeg + latitudeMin / 60 + latitudeDeg / 3600)
            End If

            Dim longitudeDeg As Double = longitudeDMS(0)
            Dim longitudeMin As Double = longitudeDMS(1)
            Dim longitudeSec As Double = longitudeDMS(2)
            Dim longitudeDouble As Double
            If String.Compare("W", longitudeRef) Then
                longitudeDouble = (longitudeDeg + longitudeMin / 60 + longitudeDeg / 3600)
            Else
                longitudeDouble = -1 * (longitudeDeg + longitudeMin / 60 + longitudeDeg / 3600)
            End If

            geoData = {latitudeDouble, longitudeDouble}
        End If

        reader.Dispose()

        Return geoData
    End Function

End Module
