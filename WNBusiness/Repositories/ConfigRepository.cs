using System.Configuration;
using WNBusiness.Models;
using WNData;
using System.Data;
using System.Linq;
using System.Collections.Generic;
namespace WNBusiness.Repositories
{
    public class ConfigRepository
    {
        DCWNDataContext DbContext = new DCWNDataContext(ConfigurationManager.ConnectionStrings["NW.GUI.Properties.Settings.LocalConnectionStringData"].ConnectionString);

        public ConfigModel GetConfiguration()
        {

            return (from a in DbContext.WNConfigs
                    select new ConfigModel() {
                        BackGroundColor = a.BackGroundColor,
                        DisplayText = a.DisplayText
                    }).FirstOrDefault();

        }

        public void SetConfiguration(ConfigModel configuration)
        {
            var confg = DbContext.WNConfigs.FirstOrDefault();

            if (confg == null) {

                confg = new WNConfig();

                DbContext.WNConfigs.InsertOnSubmit(confg);
            }
                confg.BackGroundColor = configuration.BackGroundColor;
                confg.DisplayText = configuration.DisplayText;

                DbContext.SubmitChanges();
            
        }

    }

}
