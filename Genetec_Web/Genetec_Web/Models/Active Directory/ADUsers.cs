using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Active_Directory
{
    public static class ADUsers
    {
        public static List<ADUser> cacheADUser { get; set; }

        public static List<ADUser> GetADUsers()
        {
            List<ADUser> lstADUsers = new List<ADUser>();

            try
            {
                string DomainPath = "LDAP://DC=GENETEC,DC=com";
                DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);
                DirectorySearcher search = new DirectorySearcher(searchRoot);
                search.SizeLimit = 10000;
                search.PageSize = 10000;
                search.Filter = "(&(objectClass=user)(objectCategory=person))";
                search.PropertiesToLoad.Add("samaccountname");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("objectguid");
                search.PropertiesToLoad.Add("displayname");//first name
                SearchResult result;
                SearchResultCollection resultCol = search.FindAll();
                if (resultCol != null)
                {
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        string UserNameEmailString = string.Empty;
                        result = resultCol[counter];
                        if (result.Properties.Contains("samaccountname") && result.Properties.Contains("mail") && result.Properties.Contains("displayname") && !((String)result.Properties["samaccountname"][0]).Contains('!'))
                        {
                            ADUser objSurveyUsers = new ADUser();
                            objSurveyUsers.Email = (String)result.Properties["mail"][0] +
                              "^" + (String)result.Properties["displayname"][0];
                            objSurveyUsers.UserName = (String)result.Properties["samaccountname"][0];
                            objSurveyUsers.DisplayName = (String)result.Properties["displayname"][0];
                            objSurveyUsers.GUID = new Guid((byte[])result.Properties["objectguid"][0]);
                            lstADUsers.Add(objSurveyUsers);
                        }
                    }
                }
                cacheADUser = lstADUsers;
                return lstADUsers;
            }
            catch (Exception ex)
            {
                return lstADUsers;
            }
        }

        public static List<ADUser> Search(string Text)
        {
            if (cacheADUser == null) GetADUsers();
            return cacheADUser.FindAll(x => x.DisplayName.ToLower().Contains(Text.ToLower()) || x.UserName.ToLower().Contains(Text.ToLower()));
        }
    }
}