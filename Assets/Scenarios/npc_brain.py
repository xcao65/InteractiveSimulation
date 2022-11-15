import requests
import random
import time

# query the position of the NPC twice over one second
def sample_npc_positions():
    actual_position_1 = requests.get(f'http://localhost:8342/q/scene/Game/NPC.Transform.position')
    time.sleep(0.15)
    actual_position_2 = requests.get(f'http://localhost:8342/q/scene/Game/NPC.Transform.position')

    position_1 = actual_position_1.json()[0]
    position_2 = actual_position_2.json()[0]

    return position_1, position_2

# randomly move the NPC
while True:
    x = random.randint(-30, 30)
    z = random.randint(-30, 30)

    random_position = f"{{'x': {x}, 'y': {1}, 'z':{z}}}"

    response = requests.get(f'http://localhost:8342/q/scene/Game/NPC.NPC.MoveTo("{random_position}")')

    # wait for the NPC to start moving
    time.sleep(1)

    p1, p2 = sample_npc_positions()

    # wait for NPC to stop moving
    while not (abs(p1['x'] - p2['x']) < 1 and abs(p1['z'] - p2['z']) < 1):
        p1, p2 = sample_npc_positions()
    