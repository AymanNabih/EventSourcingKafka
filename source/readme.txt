http://paulovich.net/jambo/

https://github.com/ivanpaulovich/event-sourcing-jambo

Run the following projects:
Jambo.Auth.UI.
Jambo.Consumer.UI.
Jambo.Producer.UI.

Use .../Swagger url

In Auth:
Post the following credentials:
{
  "username": "myname",
  "password": "mysecret"
}
Store the Bearer Token
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJpdmFucGF1bG92aWNoIiwic3ViIjoibXlzZWNyZXQiLCJleHAiOjE1MzMyNDE5NTQsImlzcyI6Imh0dHA6Ly9qYW1ibyIsImF1ZCI6Imh0dHA6Ly9qYW1ibyJ9.MJmEm4XFGs4R8FCR6t3-CWCGtw3i0i_z_0w2Hrw2oOE",
  "expiration": "2018-08-02T20:32:34Z"
}

In Producer:
Click Authorize, type "bearer <token>", authorize.
Use API as needed
