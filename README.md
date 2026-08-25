# In The Pipes

## Project Description

**In The Pipes** is a 2D platformer where players control Sam, a lovable but unlucky spider who gets stuck in the New York City sewer system. The goal is to explore the pipes, avoid enemies, collect items, and help Sam find his way back to the surface.

This repository contains game assets and design documentation for the project, including materials related to the game's concept, mechanics, art direction, and showcase submission.

## Demo and Media

- Trailer / showcase video: [In The Pipes Trailer](https://youtu.be/5gz01U9xEAU?si=nv3KGEZWlIMb0lmH)
- [Playable demo link](https://play.unity.com/en/games/c1ab027c-11e6-4697-84df-7fefbcffc467/in-the-pipes-v02)
  
## Gameplay Overview

Players navigate sewer-themed levels using Sam's spider abilities, including running, jumping, wall movement, web swinging, rappelling, and web shooting. Along the way, players encounter enemies such as sewer rats, snakes, and bats, while also breaking crates, collecting coins, and unlocking customization options.

The game combines platforming mechanics with a lighthearted storybook-style presentation centered on Sam's journey through the underground sewer system.

## Key Features

- 2D platformer gameplay
- Wall movement, wall bouncing, and web-based traversal
- Web shooting as an attack mechanic
- Enemy encounters with rats, snakes, and bats
- Handcrafted sewer maps designed for exploration
- Coin collection and customization systems
- Lobby/shop area for upgrades and cosmetic items
- Hand-drawn storybook sequences for narrative progression
- Leaderboard system for tracking top scores

## My Role

I served as **Team Lead** and **Lead Programmer** for the project. My work included:

- Leading project planning and development coordination
- Programming core player mechanics
- Implementing spider movement abilities
- Developing web swinging, grappling, and shooting behavior
- Creating and restructuring player and enemy state machines
- Supporting enemy behavior implementation
- Integrating Unity's new input system
- Helping transition the game from 3D components to Unity's 2D tilemap workflow
- Supporting level implementation, debugging, and gameplay polish

## Team

- Natalie Huante - Team Lead / Lead Programmer
- Mo Hijazi - Level Design / Artist
- Brian Byrd - Level Design / Artist
- Max Starreveld - Programmer

## Technologies and Tools

- Unity
- C#
- Unity DevOps
- Unity Tilemap
- Unity New Input System
- Scriptable Objects
- State machines
- Pixilart
- Adobe Photoshop
- Notability
- Adobe Premiere

## Design and Technical Notes

One of the largest technical challenges was expanding the functionality of the player and enemies while keeping the code maintainable. To address this, the project was restructured around state machines for both the player and enemy entities. This involved inheritance, scriptable objects for state data, and a transition to Unity's new input system.

The project also shifted from using 3D components to Unity's 2D tilemap system, which required restructuring assets and systems to work properly with 2D objects and colliders. The web mechanics went through several iterations before settling on a distance-joint-based system for swinging, rappelling, and grappling.

## Repository Contents

This repository may include:

- Game design documents
- Showcase submission materials
- Concept and gameplay documentation
- Art and asset references
- Screenshots or thumbnails
- Trailer/demo links
- Asset credit documentation

## Asset Credits

This project uses a combination of custom-created, modified, and third-party assets. Asset sources referenced during development include:

- Danil Chernyaev - 2D Platformer Tileset
- CraftPix - Street animal pixel art and lab tileset assets
- Free Game Assets - Forest enemies, sewerage platformer tileset, and city backgrounds
- OME6A1717 - Simple enemies bat sprites
- Elthen's Pixel Art Shop - Spider and turtle sprites
- GFragger - Sideroller sewer asset pack
- RAFMANIX - Speech bubble sprites
- Lexica - Tiger music
- XtremeFreddy / Pixabay - Bit Beats 6 music

Several assets were modified, recolored, assembled, or adjusted to better match the game's final sewer platformer theme.

## Notes

This repository is intended to preserve the creative and design materials for **In The Pipes**. For the full playable game or source implementation, add the relevant project or demo link above.
