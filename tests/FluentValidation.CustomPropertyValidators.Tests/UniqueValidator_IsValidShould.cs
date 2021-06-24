using NUnit.Framework;
using System.Collections.Generic;

namespace FluentValidation.CustomPropertyValidators.Tests
{
    [TestFixture]
    public class UniqueValidator_IsValidShould
    {
        private List<User> _users;

        [SetUp]
        public void SetUp()
        {
            _users = new List<User>
            {
                new() {Id = 1, Username = "user1", Email = "user1@domain.com"},
                new() {Id = 2, Username = "user2", Email = "user2@domain.com"},
                new() {Id = 3, Username = "user3", Email = "user3@domain.com"}
            };
        }

        [TestCase("uniqueUsername1")]
        public void IsValid_UserListNotContainsUsername_ReturnTrue(string username)
        {
            var uniqueValidator = new UniqueValidator<User, string>(x => _users.Find(i => i.Username.Equals(x.Username)));
            var validator = new TestValidator(v => v.RuleFor(x => x.Username).SetValidator(uniqueValidator));
            var user = new User { Username = username };
            var result = validator.Validate(user);

            Assert.True(result.IsValid, $"'Username({username})' should be unique.");
        }

        [TestCase("user1")]
        [TestCase("user2")]
        [TestCase("user3")]
        public void IsValid_UserListContainsUsername_ReturnFalse(string username)
        {
            var uniqueValidator = new UniqueValidator<User, string>(x => _users.Find(i => i.Username.Equals(x.Username)));
            var validator = new TestValidator(v => v.RuleFor(x => x.Username).SetValidator(uniqueValidator));
            var user = new User { Username = username };
            var result = validator.Validate(user);

            Assert.False(result.IsValid, $"'Username({username})' should not be unique.");
        }

        [TestCase("user1", 1)]
        [TestCase("user2", 2)]
        [TestCase("user3", 3)]
        public void IsValid_UserListContainsUsernameAndIdsAreEqual_ReturnTrue(string username, int id)
        {
            var uniqueValidator = new UniqueValidator<User, string>(x => _users.Find(i => i.Username.Equals(x.Username)), x => x.Id);
            var validator = new TestValidator(v => v.RuleFor(x => x.Username).SetValidator(uniqueValidator));
            var user = new User { Id = id, Username = username };
            var result = validator.Validate(user);

            Assert.True(result.IsValid, $"'Username({username})' should be unique or the id of the user with this 'Username({username})' should be equal to the 'Id({id})' parameter.");
        }

        [TestCase("user1", 2)]
        [TestCase("user2", 3)]
        [TestCase("user3", 1)]
        public void IsValid_UserListContainsUsernameAndIdsAreNotEqual_ReturnFalse(string username, int id)
        {
            var uniqueValidator = new UniqueValidator<User, string>(x => _users.Find(i => i.Username.Equals(x.Username)), x => x.Id);
            var validator = new TestValidator(v => v.RuleFor(x => x.Username).SetValidator(uniqueValidator));
            var user = new User { Id = id, Username = username };
            var result = validator.Validate(user);

            Assert.False(result.IsValid, $"'Username({username})' should not be unique and the id of the user with this 'Username({username})' should not be equal to the 'Id({id})' parameter.");
        }

        [TestCase("user1", 1, "user1@domain.com")]
        [TestCase("user2", 2, "user2@domain.com")]
        [TestCase("user3", 3, "user3@domain.com")]
        public void IsValid_UserListContainsUsernameAndIdsAndEmailsAreEqual_ReturnTrue(string username, int id, string email)
        {
            var uniqueValidator = new UniqueValidator<User, string>(x => _users.Find(i => i.Username.Equals(x.Username)), x => x.Id, x => x.Email);
            var validator = new TestValidator(v => v.RuleFor(x => x.Username).SetValidator(uniqueValidator));
            var user = new User { Id = id, Email = email, Username = username };
            var result = validator.Validate(user);

            Assert.True(result.IsValid, $"'Username({username})' should be unique or the id and email of the user with this 'Username({username})' should be equal to the 'Id({id})' and 'Email({email})' parameters.");
        }

        [TestCase("user1", 1, "user2@domain.com")]
        [TestCase("user2", 2, "user3@domain.com")]
        [TestCase("user3", 3, "user1@domain.com")]
        [TestCase("user1", 2, "user1@domain.com")]
        [TestCase("user2", 3, "user2@domain.com")]
        [TestCase("user3", 1, "user3@domain.com")]
        public void IsValid_UserListContainsUsernameAndIdsAndEmailsAreNotEqual_ReturnFalse(string username, int id, string email)
        {
            var uniqueValidator = new UniqueValidator<User, string>(x => _users.Find(i => i.Username.Equals(x.Username)), x => x.Id, x => x.Email);
            var validator = new TestValidator(v => v.RuleFor(x => x.Username).SetValidator(uniqueValidator));
            var user = new User { Id = id, Email = email, Username = username };
            var result = validator.Validate(user);

            Assert.False(result.IsValid, $"'Username({username})' should not be unique and the id or email of the user with this 'Username({username})' should not be equal to the 'Id({id})' or 'Email({email})' parameters.");
        }

    }
}
