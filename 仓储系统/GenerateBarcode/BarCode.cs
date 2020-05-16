using ZXing;
using ZXing.Common;//一维码
using ZXing.QrCode;//二维码
using System.Drawing.Imaging;//图片保存
using System.Drawing;


namespace 仓储系统.GenerateBarcode
{
    public class BarCode
    {
        /// <summary>
        /// 生成条形码
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string Code(string Name,string filepath)
        {
            //设置条形码规格
            EncodingOptions encod = new EncodingOptions();
            //设置高
            encod.Height = 120;
            //设置宽
            encod.Width = 200;
            //生成条形码的图片并保存
            BarcodeWriter wr = new BarcodeWriter();
            //指定规格
            wr.Options = encod;
            //指定条形码规格为EAN_13
            //wr.Format = BarcodeFormat.EAN_13;
            wr.Format = BarcodeFormat.CODE_39;
            //生成图片
            Bitmap img = wr.Write(Name);
            //获取文件夹的绝对路径
            //string filepath = Server.MapPath("\\image\\");
            //获取当前时间
            //string datime = DateTime.Now.ToString("yyMMddHHmmss");
            //进行图片保存
            img.Save(filepath + Name + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //返回生成一维码图片的路径
            return "/image/" + Name + ".jpg";
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string QRcode(string Name, string filepath)
        {
            //设置二维码规格
            QrCodeEncodingOptions qr = new QrCodeEncodingOptions();
            //设置编码格式,否则会乱码
            qr.CharacterSet = "UTF-8";
            qr.Height = 200;
            qr.Width = 200;
            //设置二维码图片周围空白边距
            qr.Margin = 1;
            //生成条形码图片保存
            BarcodeWriter wr = new BarcodeWriter();
            //二维码
            wr.Format = BarcodeFormat.QR_CODE;
            wr.Options = qr;
            Bitmap bit = wr.Write(Name);

            //string filepath = Server.MapPath("/image/");
            //获取当前时间
            //string datime = DateTime.Now.ToString("yyMMddHHmmss");
            //进行图片保存
            bit.Save(filepath + Name + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //返回生成一维码图片的路径
            return "/image/" + Name + ".jpg";
        }

        public bool DeleteFile(string path)
        {
            try
            {
                //string paths = Server.MapPath(path);
                bool sta = System.IO.File.Exists(path);
                if (sta)
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch //(Exception e)
            {
                //throw e;
                return false;
            }
            finally
            {

            }
        }
    }
}