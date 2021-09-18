
Feature: Get AccountController Features
   For a given bank 
   As a user
   I should be able to pass on username 
   And should be able to pass bank
   so that I get the Account information



Scenario: AccountController Account Balance information
  Given All bank information
  When  Bank Api is querired with username and bankname 
  Then  Corresponding account balance should be fetched


  Scenario: AccountController Account Transactions information
  Given All bank information
  When  Bank Api is querired with username and bankname 
  Then  Corresponding account transactons should be fetched