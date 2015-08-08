using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace PAdES_LTVTest
{
    class Program
    {
        static void Main()
        {
            const string originalPdfPath = @"C:\Users\Pavel\Desktop\PDFtest\test\test.pdf";
            const string finalPdfPath = @"C:\Users\Pavel\Desktop\PDFtest\test\test_signed.pdf";

            var certificate = PAdES_LTVSigner.CertificateHelper.ShowCertifikateDialog(StoreName.My);

            ICollection<X509Certificate2> caCertificates = new List<X509Certificate2>();
            PAdES_LTVSigner.CertificateHelper.GetCaCertificates(certificate, ref caCertificates);

            var settings = new PAdES_LTVSigner.SignatureSettings { Location = "Prague", Reason = "TestPAdESLTV"};
            settings.SetTsaClient("https://www3.postsignum.cz/DEMOTSA/TSS_user/", "demoTSA", "demoTSA2010");

            PAdES_LTVSigner.Signer.Sign(
                originalPdfPath, 
                finalPdfPath, 
                certificate, 
                caCertificates,
                settings);

            Console.WriteLine("Document signed: " + finalPdfPath);

            Console.ReadLine();
        }
    }
}
