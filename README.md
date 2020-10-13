# Promotion_Engine_SCM_CT_v1009  
Simple Promotion Engine for checkout SKUs  
  
The structure of the project is as follows:  

- Promotion_Engine_SCM_CT_v1009 - the main project folder containing the logic for the engine  
  - Core - contains the core logic
    - Business - business related functionality is stored in here (e.g. Cart functionality)
    - DataAccess - retrieving data from the in-memory database is contained in the folder
    - Database - the setup of the database context is located in this folder
    - Interfaces - interfaces for the PromotionEngine and CartBuilder are located here
  - Models - contains all models, which are used by the engine
    - DTO - Contains the Data Transfer Objects, which are used to pass data through the engine (not used yet)
  - Utilities - Helper functions are located in this subfolder
  - Program.cs - contains the main logic for starting up
- Promotion_Engine_SCM_CT_v1009_Tests - the project folder containing the tests for the engine
    
