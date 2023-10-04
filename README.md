# Event-management-system
The event management system project is a comprehensive software solution designed to facilitate the planning, organization, and execution of events. The objective is to streamline the entire event management process, from the initial event conception to post-event evaluation.
# 1. Login Page
Only admin can add users. Only admin can add user, so no need of signup page.
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/3f65fdaa-2247-4f54-acb6-6ff1ceba2a16)
# 3. Dashboard
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/d6b1cb59-8ec9-4e77-811d-8115dfd99ebf)

# 2. User page for admin to manage users (only admin role has access to this, but remaining things are the same in admin and executive member accept user page).
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/af8ec4c7-7b96-4266-88a4-272109b8871c)
# Add user pop up.
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/bd8460da-2a7c-4356-bb96-c57e8fdb6f0d)
- Admin can select the role of the user according to requirement. There are two roles, Admin and Executive Member.
- Admin create password can give it to respected user.
- Admin can not delete the user because it has relation with other tables. So, an admin can activate or inactive the user.
# 4. Let's log out from admin and login using executive member and add new event By clicking on Add Event 
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/3a578be7-ef7e-4157-927b-79667a0bd297)
# 5. Edit Event pop-up
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/588097ec-0fdf-4b27-ac8e-7cb5c6ed5e19)
# 6. In details, we can add Sponsors, Sponsors Image, Coupon type, Attendees , Expenses and EventCouponAssignmentMapping
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/e46e5020-f5eb-4161-862f-740019b69a87)
# 7. Let's start with adding sponsors.
# Sponsor Dashboard
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/90815374-47c2-4a6a-a086-c252b7a35c50)
# Add sponsor
- Add Sponsor Button
- List of sponsors
- Edit, Delete button for sponsor
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/2b05177d-6f76-42e5-b2ed-6662fb65feda)
# 8. Sponsor image
# Dashboard 
- We can add and delete images.
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/98f82222-a739-4fac-8098-881a59041cb3)

# Add image
- We can add multiple files at once.
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/ff96e851-c05b-4135-8e5e-8b5ec58d5bd1)
# 9. Coupon
# Dashboard
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/83d1015e-5701-4708-9d74-e3ac50d6b2a6)

# Add Coupon
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/f4cd9e24-b214-4eba-958e-ee75e0a620ce)
- When we add a coupon, then total numbers of coupons are generated in EventCouponAssignmentMapping, as you can see in the image 
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/86ff0957-d652-4978-ae4b-a9e0759b8b05)
# Edit
- We can delete the coupon, but we activate or deactivate it.
- We cannot change the total coupon count.
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/9201f5ce-f3b9-44ed-bbb7-a8213bc8a527)
# 10. Expenses
# Dashboard
- We can edit and delete Expenses.
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/dccf6c22-0bc9-4558-972d-945c9c489d5b)

# Add Expense
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/deda3f16-35f2-46cf-b151-c9bfef41118b)
# 11. EventCouponAssignmentMapping
- Before going to add attendees, we need to assign coupons to the Executive Member so they can assign coupons to the user. (Only executive member can assign coupon to the user)
# Dashboard
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/a5d819af-6a9a-43bd-a435-a01fa7524741)
- When you select coupon form dropdown list, you can see the coupon list 
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/75b85613-e2d1-45aa-9560-dd88139a4a6a)

- We can assign coupons to executive members in two ways.
- 1. For single coupon select EM form the dropdown list and save
  ![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/49bcb8f5-6a50-40c4-815e-457ba8c3e357)
  2. For Assigning multiple coupons at once in three steps
  ![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/70488b8f-b3e2-48a1-8543-03f60bcc01bc)
- When coupon is assigned to Attendee, then the coupon is booked and then we can not change the executive member
# 12. Attendee.
# Dashboard
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/a1d2db4b-d986-489c-84c3-04f934013bc3)

# Add Attendee
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/e9db6633-ebab-483f-81b9-83681587c15e)
-1. There are two executive members.
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/0b3b4c29-0db5-471c-aeaf-5d50fc459da1)
-2. When I select rujal we can see two coupons.
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/357879b1-3551-4f2f-b59c-390a58734f51)
-3. When I select the coupon it will show the coupon count which is assigned to rujal
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/85e0a8f2-8a4c-4c68-8d72-f07a332f86a7)
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/b5b2a32b-e9a0-4bef-8681-2f6d2762b0b7)

- We can see that rujal has three golden coupons and one silver coupon.
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/6acd851f-dc59-4968-82ea-2dac0230e1d8)
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/f539fa36-01f1-435c-94ac-2f9a67f422cc)
- We can select multiple coupons.
- Let me assign 2,3 number gold coupon to attendees.
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/804ec705-fa08-4046-9602-cf9ab62f1020)
- Now, we can see that coupons 2,3 are booked by Anshraj attendee and now we can not change executive member.
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/3b90ff7d-3740-48b8-a502-07d0b37af13d)
# Edit
- We can not change executive member and coupon but, we can remove and add coupon count 
![image](https://github.com/Patolbabu131/Event-management-system-/assets/97328289/925d2af8-ccf9-4cd7-bc03-92a3e2e072fb)


