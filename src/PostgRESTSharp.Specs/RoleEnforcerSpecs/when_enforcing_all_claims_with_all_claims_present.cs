using System;
using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using Nancy;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs
{
    public class when_enforcing_all_claims_with_all_claims_present : WithFakes
    {
        Establish that = () =>
        {
            module = An<INancyModule>();
            claims = new List<string>
            {
                "banana",
                "apple",
            };
            roleEnforcer = new RoleEnforcer();
            userIdentity = new UserIdentity();
            userIdentity.UserName = "someone";
            userIdentity.Claims = new List<string>
            {
                "db_role:banana",
                "db_role:apple",
            };
            module.Context = new NancyContext();
            module.Context.CurrentUser = userIdentity;
        };

        private Because of = () =>
        {
            exception = Catch.Exception(() => roleEnforcer.EnsureUserBelongsToRoles(module, claims));
        };

        private It should_not_have_thrown_an_exception = () =>
        {
            exception.ShouldBeNull();
        };

        private static INancyModule module;
        private static List<string> claims;
        private static RoleEnforcer roleEnforcer;
        private static UserIdentity userIdentity;
        private static Exception exception;
    }
}