Feature: TestingDurableFunction

Testing the migration of employees from the legacy application to the new one

Scenario: Migrate employees
	Given These employees in the legacy application
		| Id | First name | Last name | Email                   | Phone Number |
		| 1  | Sofie      | Blake     | sofie.blake@test.com    | +33612345678 |
		| 2  | Sion       | Britton   | sion.britton@test.com   | +33687654321 |
		| 3  | Willie     | Krueger   | willie.krueger@test.com | +33654321678 |
	When The migration process is launched
	Then These employees are saved into the new application
		| Id | First name | Last name | Email                   | Phone Number | Full name      |
		| 1  | Sofie      | Blake     | sofie.blake@test.com    | +33612345678 | Sofie Blake    |
		| 2  | Sion       | Britton   | sion.britton@test.com   | +33687654321 | Sion Britton   |
		| 3  | Willie     | Krueger   | willie.krueger@test.com | +33654321678 | Willie Krueger |



