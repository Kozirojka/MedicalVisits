# ⚠️ ATTENTION ATTENTION ⚠️

Until **commit #99**, the entire backend was implemented using **MVC API Controllers**.  
Starting from **commit #100**, the project is being migrated to **FastEndpoints**,  
where the vertical architecture will be significantly improved.

The **last stable version before the migration to FastEndpoints** is available here:  
👉 [MedicalVisits MVP 0.4](https://github.com/Kozirojka/MedicalVisits/releases/tag/mvp0.4)


## 🚀 MedOnWay
**Effortless access to healthcare from home**

## 📌 Description  
MedOnWay is an application designed to provide medical help to 
patients who are unable to visit hospitals.

## 🗺️ How it works  
1. The patient creates a request for help.  
2. The admin assigns the request to a doctor.  
3. The doctor can:  
   - Confirm the request.  
   - Cancel the request.  
   - Delegate the task to a medical assistant (nurse).
4. Doctor can view the routes on maps.



## 🫦 Features  
- CQRS pattern for separating read and write operations.  
- Clean Architecture ensuring high code maintainability.  
- JWT-based authorization with role management (Admin, Doctor, Nurse).  
- Google Maps API for real-time route optimization and travel distance calculation.  
- Automated assignment of doctors based on patient location.  

## 🔧 Technologies  
- Backend: ASP.NET Core API, Entity Framework Core  
- Frontend: React, Redux  
- Database: PostgreSQL  
- External APIs: Google Maps API (Directions API, Distance Matrix API)  

## 📞 Contact
telegram🦉: @kozirojka
phone: +380972575466
