# Emergency Evacuation

## Context
This project was created during an internship at **UiT The Arctic University of Norway in Narvik**.  

The original idea was to build an application that helps users find the closest emergency exit based on their position inside the university building.  

The scope was later redefined into a smaller version:  
- Doors are placed manually.  
- The system uses an algorithm that explores the doors according to their links and defines the shortest path to an emergency exit.  
- The application shows the user which doors to take. 

---

## Requirements / Tools
- **Unity 6000.0.49f1**  
- **Meta Quest 3**  

---

## Setup & Deployment
To deploy the project on the Meta Quest 3 headset:

1. Make sure your headset is in **Developer Mode** (see [Meta documentation](https://developers.meta.com/horizon/documentation/native/android/mobile-device-setup/)).  
2. In the headset settings (once in Developer Mode), **disable the Physical Space features**.
3. Install the **Meta Quest Link app** on your PC to connect the headset.  
   > ⚠️ Do **not** set Meta Quest Link as the active OpenXR runtime!  
   > Instead, you must use **SteamVR** as the OpenXR runtime to run the project correctly.  
4. Open the project with the correct Unity version.  
5. Go to **Build Profiles** and select **Android**.  
6. Check that the headset is detected under **Run Device**.  
7. Click **Build And Run** to deploy the app to the headset.  

---

## Usage
- In the **Hierarchy**, the first GameObject named `DEBUG` contains a script with a **checkbox**.  
- This option allows you to enable or disable **debug features**:  
  - When enabled, you can see additional virtual elements such as **doors** and **floors**.  
  - Arrows and the compass are always visible, even without debug mode.  

🎥 You can watch a short demonstration of the project here:  
[Project Demo Video](https://www.youtube.com/shorts/L11BHrLj75U)  

---

## Documentation
In the `Reports` folder, you can find two internship reports that explain most parts of the project.  
Feel free to explore and read them to better understand the work done.  

---
