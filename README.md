# Wizzarts: Magic Cardsmith ![alt text][logo]
## Meet the team 
![alt text][team]

### TABLE OF CONTENTS
### SUMMARY
### Home page
### Chat
### Membership
#### Perks
### Stores
### Events
### Tools
#### 1. Art upload
#### 2. Create card
#### 3. Create event
#### 4. Create article
#### 5. Create deck of cards
#### 6. Order deck
### Admin
#### Admin tools
### Web page special features
### Web page additional inforrmation
### Class diagram
## SUMMARY
### Upload your art, craft your play cards and build your deck
##### Member's art is uploaded and visible for other members to see. Member's art is used for participating in events, forging play cards and creating articles.
##### Wizzarts events participants are rewarded with special member title such as content creator
##### Forged play cards are assembled in decks and used for in battles with other members.
![alt text][home]

## Home page
![alt text][homeOne]
#### From the home page our clients can access member's digital art, play cards, events and articles.
![alt text][homeTwo]
#### Logged in users have access to all the Wizzarts data.
![alt text][homeThree]
#### Events are accessible to member's only
![alt text][homeFour]

## Chat
#### Chat is for members only and to join, you need to set up a nickname and pick an avatar.
#### Mmber's nickname and avatar are picked on sign up and can be eddited via the member's profile.
![alt text][chat]
#### Member's have access to various chat rooms. Our chat system support various languages.
#### Member's may address issues related to orders, game mechanics and get in touch with Wizzarts team.
![alt text][chatView]

## Membership
![alt text][member]
### Perks
#### Members participating in events will be awarded with premium membership. Premium membership can be acquired by uploading art, creating articles and events, forging play cards and participating in events.
#### Content creator is a premim role which provide access to all of Wizzarts data base and their published content is visible to everyone and ivisble on our home page.
#### Artist is a premium role which has the same benefits as the content creator. Artist will be invited to join Team wizzarts contents and become part of the team.
#### Store owners is a premium role. All store owners will acquire the premium membership as soon as they register their store location on our website.
![alt text][memberOne]
#### Each member has a portfolio wich is accessible to all team wizzarts's  members.
![alt text][memberTwo]

## Stores
#### Wizzarts's partner stores.
![alt text][store]
#### Wizzarts members who are store owners are invited to introduce their store with the help of the ongoing event "Arena masters".
![alt text][storeAdd]

## Events
![alt text][event]
### Wizzarts team invite you to join our events. We have spared some place for all interested in contributing to growing our database, wheather it is digital art, hosted events, new game mechanics.
### Special events are available to all members. Events for everyone, artists, content creators, store owners.

#### Introducing the Flavourless Event where the playcard text is missing and should be filled to fulfill the requirements for a playable playcard
![alt text][eventTwo]


#### Only members are allowed to submit an event. Each event has the possibility to store numerous sub-event information tabs.
![alt text][eventOne]


## Tools

### 1. Art upload
#### Members can submit their digital art and show it to all members.
![alt text][art]

### 2. Create card
#### Play cards created by members during the events are visible to other members
![alt text][card]

#### Cards will be added to our new game expansion
![alt text][deck]

![alt text][deckAlt]

### 3. Create event
#### Each member can create various type of events
![alt text][eventThree]

### 4. Create article
#### Articles are the best way to inform others about the art piece, the event, the card you have created.
![alt text][article] 

### 5. Create deck of cards
#### Card decks are important part of the Wizzarts game. Personalized card decks are used during our battle events.
![alt text][newCardDeck] 

### 6. Order deck
#### Card decks can be ordered and delivered to your closest Wizzarts game store or our partners stores.
![alt text][orderDeck] 

## Admin
### Admin tools
#### User with admin role already seeded :  Metzen
#### His password is :  Pa$$w0rd3  
![alt text][adminseed] 

#### Admin role can be assigned without touching the code.
#### By using the combination below and updating the new user profile, admin role will be assigned after submit.
#### user.Email == "admin@mail.com" input.Nickname == "AdminAndy"   input.PhoneNumber == "012285695439"  input.Bio == "faef3ddf-05e3-4bd3-9753-5401e2053c75"
![alt text][adminassign]

### Admin tools
#### Admin controls
![alt text][admincontrolsone] 

#### The Admin has control over events. He can update them and delete them.
![alt text][admincontrolstwo]

#### The Admin is able to re-create the default events, created during the writing of the code. By picking "flavorless" or "imageless" event type from the list of events, the "AdminEventService" will select the appropriate controller and action, then will modify the Tag helper.
![alt text][admineventcreateevent]

#### The event it self is saved along with the actionId and controllerId depending on what type of event has been picked.
![alt text][admineventcreateone]
![alt text][admineventcreatetwo]
![alt text][admineventcreatethree]

#### To avoid errors, non-admin users are allowed to create simple events therefore, they are limited to less choices. 
#### The controller will read the data and will add the tag helper (controller and action) which will allow the view to function as normal.

### Admin tools
#### The Admin has control over user's portfolio and content.
![alt text][adminuserinteractionone]

#### The admin is in charge of approving user's content and review user's portfolio following demands for collaboration.
![alt text][adminuserinteractiontwo]
![alt text][adminuserinteractionthree]

#### (Users with the appropraite role are provided with the option to join the game development after sending their applciation via mail or chat)
![alt text][adminuserinteractionfour]


### Admin tools
#### The admin is in charge of updating the user's orders
![alt text][adminapproveorder]

## Web page special features
#### It is possible to notify users about their order status by mail
![alt text][adminasengridone]
![alt text][adminasengridtwo]
![alt text][adminasengridthree]

#### All chat messages are being sanitized. All input will be sanitizied.
![alt text][sanitizeone]
![alt text][sanitizetwo]

## Web page additional inforrmation
#### Help page for those having problems with their content and for those interested in joining the team.
![alt text][help]

#### Contacts page has information about the fictional team behind the project, the source of the html template used for creating the page, the play cards.
![alt text][contact]

#### Wizzarts card game rules are listed on the "Rules" page.
![alt text][rules]

#### Wizzarts webpage terms.
![alt text][terms]

## Class diagram
### Visualization of the project's  classes and their relationships.

#### AspNetUser
![alt text][aspuser]

#### The product. Wizzart card game and its rule set.
![alt text][wizzartsgame]

#### User created Articles.
![alt text][artticles]

#### Wizzarts game play cards and decks.
![alt text][playcards]

#### Wizzarts team created events and user events.
![alt text][events]

#### Wizzarts game card deck orders
![alt text][orders]

## END

[aspuser]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/AspNetUsersFirstLevel.jpg
[wizzartsgame]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/WizzartsGame.jpg
[artticles]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/UserArticles.jpg
[playcards]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/PlayCards.jpg
[events]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/Events.jpg
[orders]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/CardOrders.jpg
[terms]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/terms.jpg
[rules]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/rules.jpg
[contact]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/ContactPage.jpg
[help]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/HelpPage.jpg
[sanitizeone]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/ChatSanitizer.jpg
[sanitizetwo]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/Sanitizer.jpg
[adminasengridone]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/SenGridAdmin.jpg
[adminasengridtwo]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/SenGridAdmin.jpg
[adminasengridthree]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/messaging.jpg
[adminapproveorder]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/AdminOrdersControls.jpg
[adminuserinteractionfour]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/AdminMemberPortfolio.jpg
[adminuserinteractionthree]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/AdminUserContentApproval.jpg
[adminuserinteractiontwo]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/AdminMembersContentForApproval.jpg
[adminuserinteractionone]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/AdminUsersView.jpg
[admineventcreateevent]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/AdminCreateEvent.jpg
[admineventcreateone]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/Event.jpg
[admineventcreatetwo]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/tagHelpActionSeed.jpg
[admineventcreatethree]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/taghelpseed.jpg
[admincontrolstwo]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/AdminEvent.jpg
[admincontrolsone]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/AdminControl.jpg
[adminassign]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/AdminAssign.jpg
[adminseed]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/AdminSeeded.jpg
[team]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/The%20team.jpg
[logo]: https://github.com/Aistonuts/CSharp_Asp.net_SoftUniProject/blob/main/Wizzarts/Web/Wizzarts.Web/wwwroot/images/navigation/MagicCardsmith.gif
[homeOne]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/HomeHomeControls.jpg
[homeTwo]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/HomeArtControls.jpg
[homeThree]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/HomeCardsControls.jpg
[homeFour]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/HomeHomeControls.jpg
[home]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/HomePage.jpg
[chat]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/HomeSignalRPage.jpg
[chatView]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/SignalRChatPage.jpg
[member]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/MembershipPage.jpg
[memberOne]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/MembersPortfolio.jpg
[memberTwo]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/MemberPortfolioContent.jpg
[store]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/StoresPage.jpg
[storeAdd]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/EventAddStore.jpg
[event]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/EventsPage.jpg
[eventOne]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/CreateEvent.jpg
[eventTwo]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/InputEvent.jpg
[art]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/ArtPage.jpg
[card]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/CardsPage.jpg
[deck]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/CardGameExpansion.jpg
[deckAlt]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/ExpansionsPage.jpg
[eventThree]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/CreateCardWithInputOnlyEvent.jpg
[article]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/CreateArticle.jpg
[newCardDeck]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/LockDeckAllowOrder.jpg
[orderDeck]: https://github.com/Aistonuts/ReadMe-Utilities/blob/main/MemberDeckOrderView.jpg
