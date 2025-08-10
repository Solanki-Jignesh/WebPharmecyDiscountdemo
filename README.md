Explanation of Create Discount Code Functionality
1. Purpose:
I created a form on the admin side to create, update, and delete discount codes with all necessary validation and business logic.

2. Key Features Implemented:

Discount Code Details:
The form captures essential fields like discount code, value, value type (percentage or fixed), minimum cart value, usage limits, applicable customers, start/end dates, and active status.

Usage Limits (Total & Per Customer):
There are two checkboxes for "Unlimited Total Usage" and "Unlimited Per Customer Usage."

When either checkbox is checked, the corresponding input field for usage count is disabled and cleared.

In this case, the backend stores a null value for those fields in the database.

null signifies unlimited usage in the business logic, allowing unlimited discount redemptions.

Applies To Customers:
There is a checkbox for "Applies To All Customers."

When this is checked, the multi-select list of individual customers is disabled and cleared.

In this scenario, an empty array (string[]) is sent to the backend, indicating that the discount applies to all customers.

When unchecked, the admin can select specific customers to apply the discount to.

Date Validation:

The form validates that the end date must be later than the start date using client-side JavaScript.

If validation fails, form submission is prevented and the user is alerted.

3. Benefits of this approach:

User-friendly UI:
Disabling input fields based on checkbox selections prevents invalid or conflicting inputs.

Clear intent in data:
Using null for unlimited usage and empty arrays for "applies to all" simplifies backend logic and makes the data self-explanatory.

Robust validation:
Ensures data integrity before hitting the server, reducing errors.

