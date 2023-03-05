# Documentation / Notes

## Guest Mode

This feature was added to serve as a fallback if for whatever reason the JHU database had issues and communication
between the app and the server was not possible.

### Notes:

* When guest mode is enabled, web requests should not be sent.
* Data that would normally be saved to the database now needs to persist across the life time of the session.
* Once the user logs out or the session ends, the information will not longer be accessible.

## Data Logging

The application currently tracks:

* Nav bar icon clicks
* Time spent viewing scenes
* Communication board category clicks
* Communication audio button clicks
* Schedule save and edit buttons clicks
* Schedule add items button (in editor) clicks
* All schedule items clicks

### Click logging:

#### Endpoint: {URL}/log/click | Method: POST

Each object attached with the `ClickLogger` script will send a request whenever the game object is clicked. The data
sent should be a json string containing the fields `user` and `button`.

### View time logging:

#### Endpoint: {URL}/log/view | Method: POST

Game objects with the script `SceneTimeLogger` attached will send requests to the backend whenever the object is
destroyed, which should only happen when the current scene unloads. The data sent should contain the fields
`user`, `viewing`, and `timeSpent`.

