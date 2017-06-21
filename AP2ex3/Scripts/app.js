var usersUri = '/api/users/';
self.addUser = function () {
    var user = {
        UserName: self.newUser.UserName(),
        Email: self.newUser.Email(),
        Password: self.newUser.Password(),
    };
    $.post(usersUri, users).done(function (item) {
        self.users.push(item);
    });
}