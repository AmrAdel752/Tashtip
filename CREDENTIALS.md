# Tashtip — Demo Login Credentials

These two accounts are seeded automatically on every app startup (see
`TASHTIP.EF2/UsersRolePolicy/Seeds/DefaultUsers.cs`, called from `Program.cs`).
They exist so the live demo is usable without registering first.

⚠️ **Change both passwords (or delete these accounts) before treating this as
a real production system** — anyone who reads this file can sign in.

## Admin

| Field    | Value               |
|----------|---------------------|
| Username | `admin`             |
| Password | `Tashtip@Admin1`    |
| Email    | admin@tashtip.com   |
| Role     | Admin (back office) |

## Demo customer

| Field    | Value              |
|----------|--------------------|
| Username | `demo`             |
| Password | `Demo@123`         |
| Email    | demo@tashtip.com   |
| Role     | Customer           |

---

Not included here on purpose: the MonsterASP.NET database connection string
and FTP/SFTP credentials — those are hosting-account secrets, not app
credentials, and don't belong in git history even in a private repo. Keep
them in the MonsterASP control panel / a local, gitignored config only.
