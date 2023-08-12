# Storm
Storm is a work-in-progress roleplay framework for S&box.

> ![IMPORTANT]
> This framework is in development and is not yet usable. Please do not use this for anything aside from development purposes.

### Layout
The framework is laid out in three main components:
* **The Framework Addon:** You are here! This is the main framework component, acting as a code library to make it really easy to update separately.
* **The Schema:** The main gamemode, using the framework and providing schema details and implementation. Many things are also dependent on json files placed in the data folder,
 such as schema details and items. A skeleton schema and a skeleton data folder will be provided for this purpose.
* **The Backend (Web):** Arguably a good way to implement a database. We're using (sometimes asynchronous) requests to send and receive data from the backend.
 All of the framework's base RPCs will be implemented here, but it shouldn't be difficult to add your own (with instructions coming later).
