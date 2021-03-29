Feature: JobListing

@mytag
Scenario: JobListing
	Given I am on https://www.labcorp.com
	And I click on 'Careers' link
	And Search for 'QA Test Automation Developer'
	When I Select 'Senior QA Test Automation Developer / Engineer' – 'Durham, North Carolina' – (posted on) '12/10/2020' 
	And Confirm job title, job location, and job id '20-85412'
	And Confirm first sentence of third paragraph under Description/Introduction 'The right candidate for this role will participate in the test automation technology development and best practice models.'
	And Confirm second bullet point under Management Support as 'Prepare test plans, budgets, and schedules.'
	And Confirm third requirement as '5+ years of experience in QA automation development and scripting.'
	And Confirm first suggested automation tool to be familiar with contains 'Selenium'
	Then Click Apply Now and confirm points 5 and 6 in the proceeding page. 
	And Click to Return to Job Search
