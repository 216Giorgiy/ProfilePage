﻿using System;
using Core.Model;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Core.Tests.Model
{
    [Binding]
    public class ProfileLevelSteps
    {
        ProfileLevelService _service;
        ProfileLevel _result;

        [Given(@"An instance of ProfileService")]
        public void GivenAnInstanceOfProfileService()
        {
            _service = new ProfileLevelService();
        }
        
        [When(@"I request a level for (.*) points")]
        public void WhenIRequestALevelForPoints(int p0)
        {
            _result = _service.GetLevelForPoints(p0);
        }
        
        [Then(@"the result should be level (.*)")]
        public void ThenTheResultShouldBeLevel(int p0)
        {
            Assert.AreEqual(p0, _result.Level);
        }
    }
}
