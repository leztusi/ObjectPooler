# Object Pooler

Object Pooling help optimizing games by instantiating all the recycable prefabs on limited amount then calling them when needed instead of instantiating and destroy.

# Usage

* Make sure you have the Object Pooler Prefab located at 'Resources' folder.
* You can add prefabs on Object Pooler Prefab to call later.
* each pooled prefabs should have "ObjectPoolerPoolBack" script to Pool back the object.
* You Grab the pooled object with

Here's a line for us to start with.

This line is separated from the one above by two newlines, so it will be a *separate paragraph*.

This line is also a separate paragraph, but...
This line is only separated by a single newline, so it's a separate line in the *same paragraph*.

`GrabObject(int Index, Vector3 Position, Quaternion Rotation)`

or

`GrabObject(string Name, Vector3 Position, Quaternion Rotation)`
