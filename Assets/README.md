Elevator Simulation (Unity)
Overview

This project simulates a simple multi-elevator system in Unity.
The system manages 3 elevators across 4 floors and assigns the most suitable elevator when a floor request is made.

The goal was to demonstrate basic elevator scheduling logic, request queues, and direction-based elevator calls.

Features

3 elevators

4 floors (Ground, 1, 2, 3)

Smooth elevator movement between floors

Current floor display on each elevator

Elevator request queue using Queue<int>

Two calling systems are implemented:

1. Single Call Button

Each floor has a Call Lift button which requests an elevator to that floor.

2. Direction Buttons

Floors have Up / Down buttons depending on the floor level.

Example:

Floor 3  ↓
Floor 2  ↑ ↓
Floor 1  ↑ ↓
Ground   ↑

This version uses function overloading to support direction-based requests.

How Elevator Selection Works

When a request is made, the ElevatorManager selects the best elevator based on:

Distance from the requested floor

Current elevator direction

Number of pending requests in the queue

Each elevator processes its requests in order.

Project Structure

Main scripts:

ElevatorManager.cs – Handles elevator selection and request assignment

Elevator.cs – Controls elevator movement and request queue

FloorButton.cs – Handles button input from the UI

Direction.cs – Enum representing elevator direction (Idle / Up / Down)

Scenes

MainMenu – Scene selection menu

Elevator_CallSystem – Single call button system

Elevator_DirectionSystem – Up / Down button system