# Task 4
# SRS
## 1. Functional Requirements.
Functional requirements are a set of features that a software or system must possess in order to fulfill its intended purpose.
These requirements describe the behavior of the system and the features it must have to meet the needs of its users. 

### 1.1 User Profile / Auth
   * User is able to create his account (registration with email confirmation);
   * User is able to log in with created account or using Google/Facebook account;
   * User is able to change his password and nickname.

### 1.2 Users Relationships
   * User can configure hist public profile which includes:
      * Short bio;
      * Social networks links/nicks;
      * Avatar.
   * User can add invite other users by email;
   * User can send friend request through nickname;
   * User can respond to friend request (approve/reject).

### 1.3 Quizzes Management
* User is able to create new quiz:
   * name
   * cover image (optional),
   * genres: select from drop-down;
   
* User can add tracks to the quiz:
   * using link to Spotify playlist;
   * add tracks manually using search functionality (name/author);
  
* User can configure quiz settings:
 * Track playback time (5 - 25 seconds, default value: 15);
 * Answer time (10 - 40 seconds, default value: playback time + 10s);
 * Answer type - select from proposed answers or enter answer (author + name) in the field;

* User can control accessibility of quiz:
 * Public - anyone can access it through catalog or link;
 * Friends - quiz is accessible only for user's friends.

### 1.4 Quizzes Catalog
* User can select a random public quiz to play;
* User can see list of public quizzes;
* User can apply filters:
   * Genres;
   * Track playback time;
   * Answer time;
   * Answer type;
* User can see his friends quizzes.
 
### 1.5 Playing a quiz
* User is able to start playing when ready;
* User is able to see timer and stage;
* After timeout or accepting user answer, correct one is shown for 5 seconds and then user is moved to the next track;
* After completing the quiz user sees his personal result and leaderboard;
* User can share his result with friends or in social networks.

## 2. Non-functional requirements

Non-functional requirements refer to characteristics or qualities that a system must possess or adhere to in order to meet its intended purpose, but they do not directly relate to the system's behavior or functionality.

* Adaptive web design (different resolutions);
* Intuitive user experience;
* High performance;
* Scailability and maintainability;
* Reliability - response time / availability;
* Security - users data protected;
* Localization;
* Portability;
* Day/Night modes;
* Quality/Stability;
