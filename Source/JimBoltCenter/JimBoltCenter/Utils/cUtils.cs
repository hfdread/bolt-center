using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JimBoltCenter.Forms;
using DBMapping.BOL;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;
using DevExpress.XtraEditors;

namespace JimBoltCenter.Utils
{
    public static class cUtils
    {
        public static string AMOUNT_FMT = "#,##0.000";
        public const string FMT_DATE1 = "MMM dd, yyyy";
        public const string FMT_ID = "0000000";
        public const string FMT_ORNUMBER = "000000";
        public const string FMT_CURRENCY_AMT = "#,##0.000";
        public const string FMT_NUMBER = "#,##0";

        public static string FILEPATH = ConfigurationManager.AppSettings.Get("filepath");

        public static string GenerateFilterCondition(string sFilter)
        {
            string[] tokens = null;
            StringBuilder ret = new StringBuilder();
            tokens = sFilter.Split(' ');
            ret.Append("%");
            for (int i = 0; i < tokens.Length; i++)
            {
                ret.Append(tokens[i]);
                ret.Append("%");
            }
            return ret.ToString();
        }

        public static double GetLastDiscountPrice(string DiscountFormat, double Price)
        {
            if (DiscountFormat.Length > 0)
            {
                while (DiscountFormat.Substring(DiscountFormat.Length - 1) == "/")
                {
                    DiscountFormat = DiscountFormat.Remove(DiscountFormat.Length - 1);
                }

                string[] discounts = DiscountFormat.Split('/');
                double prevPrice = 0, discount = 0, discountValue = 0;

                foreach (string item in discounts)
                {
                    if (item == "/" || item == "")//ignore
                    {
                        continue;
                    }

                    discount = ConvertToDouble(item);
                    if (prevPrice == 0)//first iteration
                    {
                        discountValue = (discount / 100) * Price;
                        if (item.Contains('+'))
                        {
                            prevPrice = Price + discountValue;
                        }
                        else
                        {
                            prevPrice = Price - discountValue;
                        }
                    }
                    else
                    {
                        discountValue = (discount / 100) * prevPrice;
                        if (item.Contains('+'))
                        {
                            prevPrice += discountValue;
                        }
                        else
                        {
                            prevPrice -= discountValue;
                        }
                    }

                    prevPrice = Math.Round(prevPrice, 3);
                }

                return prevPrice;
            }
            else
                return Price;
        }

        public static string GetDiscountedPrices(string DiscountFormat, double Price)
        {
             //remove any leading and any duplicate /

            if (DiscountFormat.Length > 0)
            {
                //trailing slashes
                while (DiscountFormat.Substring(DiscountFormat.Length - 1) == "/")
                {
                    DiscountFormat = DiscountFormat.Remove(DiscountFormat.Length - 1);
                }

                //leading slashes
                while (DiscountFormat.Substring(0) == "/")
                {
                    DiscountFormat = DiscountFormat.Remove(0);
                }

                StringBuilder sRet = new StringBuilder(); ;
                string[] discounts = DiscountFormat.Split('/');
                double prevPrice = 0, discount = 0, discountValue = 0;

                foreach (string item in discounts)
                {
                    if (item == "/" || item == "")//ignore
                    {
                        continue;
                    }

                    discount = ConvertToDouble(item);
                    if (prevPrice == 0)//first iteration
                    {
                        discountValue = (discount / 100) * Price;
                        if (item.Contains('+'))
                        {
                            prevPrice = Price + discountValue;
                        }
                        else
                        {
                            prevPrice = Price - discountValue;
                        }
                    }
                    else
                    {
                        discountValue = (discount / 100) * prevPrice;
                        if (item.Contains('+'))
                        {
                            prevPrice += discountValue;
                        }
                        else
                        {
                            prevPrice -= discountValue;
                        }
                    }

                    sRet.Append(Math.Round(prevPrice, 3).ToString(FMT_CURRENCY_AMT));
                    sRet.Append("/");
                }

                sRet.Remove(sRet.Length - 1, 1);
                return sRet.ToString();
            }
            else
                return Price.ToString(FMT_CURRENCY_AMT);
        }

        public static int ConvertToInteger(dynamic value)
        {
            try
            {
                if (value == null || value is System.DBNull)
                    return 0;
                else if (value == "")
                    return 0;
                else if (value.Trim().Length <= 0)
                    return 0;
                else
                {
                    if (value.Contains(".000"))
                    {
                        value = value.Replace(".000", "");
                    }
                }

                return Convert.ToInt32(value.Trim());
            }
            catch (Exception ex)
            {
                string err = string.Format("value in convertion {0}\n{1}", value, ex.Message);
                ShowMessageError(err , "Error in ConvertToInteger");

                return 0;
            }
        }

        public static double ConvertToDouble(dynamic value)
        {
            if (value == null)
                return 0;
            else if (value == "")
                return 0;
            else if (value.Trim().Length <= 0)
                return 0;
            else
                return Convert.ToDouble(value);
        }

        public static DialogResult ShowMessageError(string msg, string title)
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowMessageInformation(string msg, string title)
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowMessageExclamation(string msg, string title)
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult ShowMessageQuestion(string msg, string title)
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static MainForm getMainForm()
        {
            return MainForm.m_FormInstance;
        }

        public static string GenerateItemFilter(string sItemName, ItemSizes size)
        {
            string filter = "";
            if (sItemName != "")
                filter += string.Format("(I.Name like '%{0}%' or I.Code like '%{0}%')", sItemName);

            if (size != null && sItemName != "")
                filter += string.Format(" and I.Size.ID={0}", size.ID);
            else if (size != null && sItemName == "")
                filter += string.Format(" I.Size.ID={0}", size.ID);

            return filter;
        }

        public static bool AmountInputMask(char key, TextEdit textbox)
        {
            bool bHandled = false;

            if (!char.IsDigit(key) && key != '.')
            {
                if(!char.IsControl(key))
                    bHandled = true;
            }

            if (key == '.' && textbox.Text.IndexOf('.') > -1)
                bHandled = true;

            return bHandled;
        }

        public static bool DigitInputMask(char key, TextEdit textbox)
        {
            bool bHandled = false;

            if (!char.IsDigit(key) && !char.IsControl(key))
                bHandled = true;

            return bHandled;
        }

        public static IList<SortedSize> GetSortedSizeTable(IList<ItemSizes> sizeList)
        {
            IList<SortedSize> sortedSizeList = new List<SortedSize>();
            Regex regx = new Regex(@"[a-zA-Z]");
            foreach (ItemSizes size in sizeList)
            {
                SortedSize _sortedSize = new SortedSize();
                _sortedSize.Description = size.Description;
                _sortedSize.ID = size.ID;

                //default values for sorting
                _sortedSize.sort1 = 0;
                _sortedSize.sort2 = 0;
                _sortedSize.sort3 = 0;
                _sortedSize.sort4 = 0;
                
                //split the description
                string sizeDesc = size.Description;
                sizeDesc = sizeDesc.ToLower();
                sizeDesc = sizeDesc.Replace("mm","");
                sizeDesc = sizeDesc.Replace("cm", "");
                sizeDesc = sizeDesc.Replace("km", "");

                string[] sSplit = sizeDesc.Trim().Split('x');

                foreach (string split in sSplit)
                {
                    double value = 0, mod=0,numerator=0, denominator=0;
                    int wholeNumber=0;
                    string[] fraction, operators;

                    if (split.Contains("-"))
                    {
                        value = 0;
                    }
                    else if(regx.Matches(split).Count > 0)
                    {
                        value = 0;
                    }
                    else if (split.Contains("/"))//check if fraction
                    {
                        if (split.Trim().Contains(' ')) //with whole number fraction e.g 1 1/2
                        {
                            fraction = split.Trim().Split(' ');
                            wholeNumber = ConvertToInteger(fraction[0].Trim());

                            operators = fraction[1].Split('/');
                            numerator = ConvertToDouble(operators[0].Trim());
                            denominator = ConvertToDouble(operators[1].Trim());
                            mod = numerator/denominator;
                            value = wholeNumber + mod ;
                        }
                        else //fraction only e.g 1/4, 1/2
                        {
                            fraction = split.Trim().Split('/');
                            numerator = ConvertToInteger(fraction[0].Trim());
                            denominator = ConvertToInteger(fraction[1].Trim());

                            value = numerator/denominator;
                        }
                    }
                    else //not a fraction e.g 25mm, 50cm, 55
                    {
                        value = ConvertToDouble(split.Trim());
                    }
                    
                    if (_sortedSize.sort1 == 0)
                    {
                        _sortedSize.sort1 = value * 1000;
                    }
                    else if (_sortedSize.sort2 == 0)
                    {
                        _sortedSize.sort2 = value * 100;
                    }
                    else if (_sortedSize.sort3 == 0)
                    {
                        _sortedSize.sort3 = value * 10;
                    }
                    else if (_sortedSize.sort4 == 0)
                    {
                        _sortedSize.sort4 = value * 1;
                    }
                }

                sortedSizeList.Add(_sortedSize);
            }

            var query = (from sorted in sortedSizeList
                        orderby sorted.sort1,sorted.sort2, sorted.sort3, sorted.sort4 ascending
                        select sorted).ToList<SortedSize>();

            return query;
        }

        public static void CreateCrashLog(string message, string processName, string featureName)
        {
            //message = crash message/exception.
            //processName = name of the transaction made e.g Add, Edit, Save, Delete, and etc.
            //featureName = name of the feature that the error occurred e.g Invoice, Receipt, and etc.

            StreamWriter sw = new StreamWriter(string.Format("{0}\\crash.log",FILEPATH),true,Encoding.Unicode);

            sw.WriteLine(string.Format("==============================================\nFeature: {0}  [{1}]\nProcess: {2}\nInformation:\n{3}\n==============================================\n"
                                        ,featureName, DateTime.Now ,processName, message));

            sw.Flush();
            sw.Close();
        }

        public static double GetVATAmount(double amount)
        {
            return Math.Round(amount / 1.12 * 0.01, 2);
        }
    }
}
