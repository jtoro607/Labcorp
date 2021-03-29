using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace LabcorpPOMs
{
    public class WebElementPOM
    {
        public WebElementPOM()
        {

        }
        public By CareerLinkLocator => By.LinkText("http://www.labcorpcareers.com/");
        public By testLocator => By.XPath("//*[@id='block-footer']/ul/li[10]/a");
        public By SearchFieldLocator => By.Id("search-keyword-af6e43073b");

        //public IWebElement => 
    }
}
