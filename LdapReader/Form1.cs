﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Principal;
using System.Collections;
using System.DirectoryServices.Protocols;
using System.Net;
using LdapAndAdLibrary;

namespace LdapReader
{
    public partial class Form1 : Form
    {
        DataTable dtAttribute { get; set; }

        public Form1()
        {
            InitializeComponent();

            dtAttribute = new DataTable();
            dtAttribute.Columns.Add("Name");
            dtAttribute.Columns.Add("Value");
            dataGridViewAttribute.DataSource = dtAttribute;

            textBoxPath.Text = ConfigurationManager.AppSettings["Path"];
            textBoxAccount.Text = ConfigurationManager.AppSettings["Account"];
            textBoxPassword.Text = ConfigurationManager.AppSettings["Password"];
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBoxMessage.Clear();

                //LdapConnection ldapCnt = new LdapConnection(textBoxPath.Text);

                ////可以使用 DN or 含domain User
                //var dnUser = @"CN=rainmaker_ho,OU=EMP,OU=RM,DC=rm,DC=com,DC=tw";
                //var domainUser = "rainmaker_ho@rm.com.tw";
                //var domainUser2 = @"rm_domain\rainmaker_ho";
                //if (string.IsNullOrEmpty(textBoxPassword.Text))
                //{
                //    throw new Exception("密碼不能為空值!");
                //}
                //ldapCnt.Credential = new NetworkCredential(textBoxAccount.Text, textBoxPassword.Text);
                //ldapCnt.Bind(); // 登入

                LdapHelper ldapUtility = new LdapHelper(textBoxPath.Text, textBoxAccount.Text, textBoxPassword.Text);
                richTextBoxMessage.AppendText("登入成功");
            }
            catch (Exception ex)
            {
                richTextBoxMessage.AppendText("登入失敗");
                richTextBoxMessage.AppendText(ex.ToString());
            }
            finally
            {
                richTextBoxMessage.ScrollToCaret();
            }
        }


        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBoxMessage.Clear();

                LdapHelper ldapUtility = new LdapHelper(textBoxPath.Text, textBoxAccount.Text, textBoxPassword.Text);
                ResultCode result = ldapUtility.Delete(textBoxDnToDelete.Text);
                if (result == ResultCode.Success)
                {
                    richTextBoxMessage.AppendText("刪除成功");
                }
                else
                {
                    richTextBoxMessage.AppendText("刪除失敗");
                    richTextBoxMessage.AppendText(result.ToString());
                }
            }
            catch (Exception ex)
            {
                richTextBoxMessage.AppendText(ex.ToString());
            }
            finally
            {
                richTextBoxMessage.ScrollToCaret();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBoxMessage.Clear();

                LdapHelper ldapUtility = new LdapHelper(textBoxPath.Text, textBoxAccount.Text, textBoxPassword.Text);
                List<string> result = ldapUtility.Search(textBoxDnToDelete.Text, null);

                foreach (var item in result)
                {
                    richTextBoxMessage.AppendText(item + Environment.NewLine);
                }
                //List<Dictionary<string, string>> result = ldapUtility.Search(textBoxDnToDelete.Text, null);
                //foreach (Dictionary<string, string> dictionary in result)
                //{
                //    foreach (KeyValuePair<string, string> item in dictionary)
                //    {
                //        richTextBoxMessage.AppendText($"{item.Key}={item.Value}\n");
                //    }
                //}
            }
            catch (Exception ex)
            {
                richTextBoxMessage.AppendText(ex.ToString());
            }
            finally
            {
                richTextBoxMessage.ScrollToCaret();
            }
        }
        /*
private void buttonQueryAll_Click(object sender, EventArgs e)
{
try
{
richTextBoxMessage.Clear();

DirectoryEntry root = new DirectoryEntry(textBoxPath.Text, textBoxAccount.Text, textBoxPassword.Text);
DirectorySearcher search = new DirectorySearcher(root);
SearchResultCollection results = search.FindAll();

if (results == null)
{
richTextBoxMessage.AppendText("No data.");
return;
}

foreach (SearchResult result in results)
{
ResultPropertyCollection rpc = result.Properties;

List<string> nameList = new List<string>(rpc.PropertyNames.Count);
foreach (string name in rpc.PropertyNames)
{
nameList.Add(name);
}
nameList = nameList.Distinct().OrderBy(x => x).ToList();

foreach (string name in nameList)
{
ResultPropertyValueCollection rpvc = rpc[name];
int valueCount = rpvc.Count;
foreach (object value in rpvc)
{
if (value == null)
    continue;

string valueString = ExtractAttributValue(name, value);
richTextBoxMessage.AppendText(string.Format("{0}={1}\n", name, valueString));
}
}

richTextBoxMessage.AppendText("\n----------------------------------------------------------\n\n");
richTextBoxMessage.ScrollToCaret();
}

richTextBoxMessage.AppendText($"Count: {results.Count}");
}
catch (Exception ex)
{
richTextBoxMessage.AppendText(ex.ToString());
}
finally
{
richTextBoxMessage.ScrollToCaret();
}
}

private void buttonQuery1_Click(object sender, EventArgs e)
{
try
{
richTextBoxMessage.Clear();
dtAttribute.Clear();

DirectoryEntry root = new DirectoryEntry(textBoxPath.Text, textBoxAccount.Text, textBoxPassword.Text);
DirectorySearcher search = new DirectorySearcher(root);
SearchResult result = search.FindOne();

if (result == null)
{
richTextBoxMessage.AppendText("No data.");
return;
}

ResultPropertyCollection rpc = result.Properties;

List<string> nameList = new List<string>(rpc.PropertyNames.Count);
foreach (string name in rpc.PropertyNames)
{
nameList.Add(name);
}
nameList = nameList.Distinct().OrderBy(x => x).ToList();

foreach (string name in nameList)
{
ResultPropertyValueCollection rpvc = rpc[name];
int valueCount = rpvc.Count;
foreach (object value in rpvc)
{
if (value == null)
continue;

string valueString = ExtractAttributValue(name, value);

richTextBoxMessage.AppendText(string.Format("{0}={1}\n", name, valueString));

DataRow drAttribute = dtAttribute.NewRow();
drAttribute["Name"] = name;
drAttribute["Value"] = valueString;
dtAttribute.Rows.Add(drAttribute);
}
}

dataGridViewAttribute.Sort(dataGridViewAttribute.Columns[0], ListSortDirection.Ascending);
}
catch (Exception ex)
{
richTextBoxMessage.AppendText(ex.ToString());
}
finally
{
richTextBoxMessage.ScrollToCaret();
}
}

private static string ExtractAttributValue(string name, object value)
{
string valueString;
if (name.Contains("objectguid") && value is byte[] && ((byte[])value).Length == 16)
{
Guid guid = new Guid(value as byte[]);
valueString = guid.ToString();
}
else if (name == "objectsid" && value is byte[])
{
// A security identifier (SID) is used to uniquely identify a security principal or security group.
// Security principals can represent any entity that can be authenticated by the operating system, such as a user account, a computer account, or a thread or process that runs in the security context of a user or computer account.
// https://docs.microsoft.com/en-us/windows/security/identity-protection/access-control/security-identifiers
// https://www.lijyyh.com/2015/08/sid-deep-dive.html
SecurityIdentifier sid = new SecurityIdentifier((byte[])value, 0);
valueString = sid.Value;
}
else
{
valueString = value.ToString();
}

return valueString;
}
*/
    }
}