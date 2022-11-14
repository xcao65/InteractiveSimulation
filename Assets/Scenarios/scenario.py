import requests
import json

# get id of a coin (pickup) game object
r = requests.get('http://localhost:8342/q/scene/Game/Pickup[2].Pickup.ID')
coin_id = r.json()[0]

# find the coin's position
r = requests.get(f'http://localhost:8342/q/scene/Game/Pickup[Pickup.ID="{coin_id}"].Transform.position')

coin_position = r.json()[0]

print("robot go get coin")

# robot pick up coin
r = requests.get(f'http://localhost:8342/q/scene/Game/Robot.Player.MoveTo("{coin_position}")')

print("waiting for robot to get coin...")

# wait until the robot picks up the coin
r = requests.get(f'http://localhost:8342/q/scene/Game/Pickup.Pickup[ID="{coin_id}"]')
while len(r.json()) > 0:
    r = requests.get(f'http://localhost:8342/q/scene/Game/Pickup.Pickup[ID="{coin_id}"]')

print("robot got coin!")

print("moving robot to new position...")

# robot move to position
pos = "{'x': 0, 'y': 2, 'z': -10}"
r = requests.get(f'http://localhost:8342/q/scene/Game/Robot.Player.MoveTo("{pos}")')

print("human go get coin")

# human go get coin
pos = "{'x': 0, 'y': 2, 'z': -15}"
r = requests.get(f'http://localhost:8342/q/scene/Game/Human.Player.MoveTo("{pos}")')
