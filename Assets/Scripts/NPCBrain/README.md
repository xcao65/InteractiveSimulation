# NPC Brain

Run the NPC brain to tell the NPC in the Tutorial scene what to do.

## Setup
Make sure you have python installed.

1. Run `python3.7 -m venv ./.venv` to create your python virtual environment, or follow [this documentation](https://docs.python.org/3/library/venv.html) and set up your virtual environment in a `.venv/` folder.

2. Activate the virtual environment, e.g. `source .venv/bin/activate`.

3. Install dependencies: `pip install -r requirements.txt`.

## Run the NPC brain
Once your python virtual environment is set up and activated, you can run the NPC brain.

Play the Tutorial scene in Unity and then run `python npc_brain.py` in the terminal in your virtual environment.

You should see the NPC in the Unity player moving around.
