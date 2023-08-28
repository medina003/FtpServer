using System.Net;
using System.IO;
int bufferSize = 2048;
FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(@"ftp://127.0.0.1/FTPSITE/file.txt"); //I've used filezilla server
ftpRequest.Credentials = new NetworkCredential("user1", "admin123");
ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
ftpRequest.UseBinary = true;
ftpRequest.UsePassive = true;
ftpRequest.KeepAlive = true;
FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
Stream ftpStream = ftpResponse.GetResponseStream();
FileStream localFileStream = new FileStream(@"C:\new\new.txt", FileMode.Create);
byte[] byteBuffer = new byte[bufferSize];
int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
try
{
    while (bytesRead > 0)
    {
        localFileStream.Write(byteBuffer, 0, bytesRead);
        bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
localFileStream.Close();
ftpStream.Close();
ftpResponse.Close();
ftpRequest = null;
Console.Write("success");
