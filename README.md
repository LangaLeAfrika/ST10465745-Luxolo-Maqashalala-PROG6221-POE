# CyberSecurityBot 🤖

## 📌 Project Overview
# Cybersecurity Awareness Chatbot Version 3.0

YouTube: https://youtu.be/Quh5d7rg6-M
---
This project is a **Cybersecurity Awareness Chatbot** developed using **C# and WPF**. The application is designed to educate users about cybersecurity concepts through interactive conversation, a quiz system, and task management features.

The chatbot simulates a virtual assistant that can answer cybersecurity-related questions, test user knowledge, and help users manage simple tasks.

---

## 🎯 Objectives

* Provide cybersecurity awareness through an interactive chatbot
* Implement a quiz system for knowledge testing
* Store and manage user tasks using JSON
* Log user activity for tracking interactions
* Demonstrate object-oriented programming concepts

---

## ⚙️ Features

### 💬 Chatbot System

* Responds to user input using keyword-based logic
* Covers cybersecurity topics such as:

  * Phishing
  * Password safety
  * Malware
* Provides informative and user-friendly responses

---

### 🧠 Quiz System

* Contains 10–12 cybersecurity questions
* Supports:

  * Multiple choice (A/B/C/D)
  * True/False questions
* Features:

  * One question at a time
  * Immediate feedback (Correct/Incorrect)
  * Explanations for answers
  * Score tracking
  * Final performance summary

---

### 📝 Task Manager

* Add new tasks
* View saved tasks
* Persist tasks using a `tasks.json` file
* Automatically loads saved tasks on startup

---

### 📊 Activity Logger

* Logs important user actions such as:

  * Starting the quiz
  * User interactions
* Saves logs to `activity_log.txt`

---

### 🎨 User Interface (WPF)

* Chat display area
* Input textbox
* Send button
* Clean formatting with timestamps
* Auto-scrolling chat window

---

## 🏗️ System Architecture

The application is structured using multiple classes:

* **ChatBot.cs** → Handles user interaction and responses
* **QuizManager.cs** → Controls quiz flow and scoring
* **QuizQuestion.cs** → Represents quiz questions
* **TaskManager.cs** → Manages task storage and retrieval
* **ActivityLogger.cs** → Logs system activity
* **MainWindow.xaml.cs** → Handles UI interaction

---

## 💻 Technologies Used

* C# (.NET WPF)
* JSON (data storage)
* Newtonsoft.Json library
* File handling (StreamWriter, File IO)

---

## ▶️ How to Run the Application

1. Open the project in **Visual Studio**
2. Restore NuGet packages (ensure Newtonsoft.Json is installed)
3. Build the solution
4. Run the application

---

## 🧪 How to Use the Application

### Start Chatting

Type a message in the input box and press **Send**.

---

### Start Quiz

```bash
quiz
```

Then:

```bash
next question
```

Answer using:

```bash
A / B / C / D
```

or

```bash
True / False
```

---

### Manage Tasks

```bash
add task
view tasks
```

---

### Example Questions

* "What is phishing?"
* "How can I create a strong password?"

---

## ⚠️ Error Handling

The application includes exception handling to:

* Prevent crashes
* Handle invalid inputs
* Ensure smooth user experience

---

## 📂 File Structure

```
CyberSecurityChatbot/
│
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── ChatBot.cs
├── QuizManager.cs
├── QuizQuestion.cs
├── TaskManager.cs
├── ActivityLogger.cs
│
├── tasks.json
├── activity_log.txt
└── type.wav
```
## 📊 Version Comparison Table

| Feature / Functionality | Version 1              | Version 2                     | Version 3 (Current)                    |
| ----------------------- | ---------------------- | ----------------------------- | -------------------------------------- |
| User Interface          | Console-based          | WPF                           | WPF Graphical User Interface           |
| Chatbot Responses       | Basic keyword matching | Improved structured responses | Interactive and formatted responses    |
| Cybersecurity Awareness | Basic topics           | Expanded topics               | Fully integrated with quiz system      |
| Task Management         | Not included           | Add and view tasks            | Full task system with persistence      |
| Data Storage            | None                   | JSON storage (tasks.json)     | JSON + logging system                  |
| File Handling           | Not implemented        | Read/write JSON files         | JSON + activity log file               |
| Quiz System             | Not included           | Not included                  | Full quiz with scoring and feedback    |
| Score Tracking          | Not included           | Not included                  | Implemented                            |
| Activity Logging        | Not included           | Not included                  | Logs user actions to file              |
| Error Handling          | Minimal                | Basic                         | Robust (try-catch implemented)         |
| Code Structure          | Basic classes          | Improved modular structure    | Fully modular and scalable             |
| User Experience         | Simple interaction     | Improved functionality        | Interactive, responsive, user-friendly |

---

## ✅ Version 3.0 Requirements Covered

✔ Chatbot interaction
✔ Quiz implementation
✔ JSON data storage
✔ Activity logging
✔ WPF user interface
✔ Error handling
✔ Object-oriented design

---

## 🚀 Future Improvements

* Enhanced NLP (Natural Language Processing)
* Improved UI styling (themes/dark mode)
* Database integration instead of JSON
* Voice interaction support

---

## 📚 References

Troelsen, A. and Japikse, P. 2021. Pro C# 9 with .NET 5: Foundational Principles and Practices in Programming. 10th ed. Apress

Microsoft, (n.d.) *C# Documentation*. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/ (Accessed: June 2026).

Microsoft, (n.d.) *Windows Presentation Foundation (WPF) Documentation*. Available at: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/ (Accessed: June 2026).

Newtonsoft, (n.d.) *Json.NET Documentation*. Available at: https://www.newtonsoft.com/json (Accessed: June 2026).

OpenAI, (2026) *ChatGPT*. Available at: https://www.openai.com/chatgpt (Accessed: June 2026).

---

## 👨‍💻 Author
## Luxolo Maqashalala

Developed as part of the **PROG6221 POE**.

---

<img width="800" height="800" alt="image" src="https://github.com/user-attachments/assets/d26d79ce-464b-4880-a190-f02920b8ba69" />


