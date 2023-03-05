#Project Notes/Important Information

##Contact Information for Future Contributors
If you have any questions regarding the project please email me at joshuabetz12@gmail.com and I will be happy to assist!

##Story Handling
Outline and directions of how stories should be made and how they are handled.

###Story Handling
* All story related assets are located in the `Assets/Resources/Stories` directory of the project.
* Each story should have a folder inside of the above directory.
* Each story needs a Text.json file in the root of it's directory, as well as an `Images` and `Audio` folder.
* Once a user selects their desired story, the assets within it's corresponding folder will be loaded in at runtime.
* The loaded assets are then stored within the `StoryManager` GameOb ject and used in theapplication.
* This system prevents us from needing to manually create the stories, instead its done purely from the assets.

###How do I make a new story?
* You can easily make a new story by navigating to the `Assets/Resources/Stories` directory and creating a new folder with the desired story name.
* Then, inside of that folder create a `Text.json` file and two directories called `Images` and `Audio`.
* The `Text.json` should have this format:
```
{
	"title": "Story Title"
	"lines": ["Array", "of", "strings"]
}
```
* Then drag and drop the images and audio files into their corresponding folders.
* Rename the audio and image files numerically like: 01, 02, 03...
* They need to be named this way so the images and audio files are loaded in the sequence they would appear in the story.
* The final step is just to add a new `StoryCard` object to the `Story List` inside of the `Home` scene. You then need to add a click action to the story card button so the story can load.
