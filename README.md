# page-replacement-sim
Simulator for page replacement in frames made for Academic purposes.

Sample
```
Frames? 4
Number of page requests? 12

Page requests?
1 2 3 4 1 2 5 1 2 3 4 5
Strategy (lru, fifo, optimal)? fifo
Requesting page 1
=====
| 1 |
-----
| - |
-----
| - |
-----
| - |
=====

Requesting page 2
=====
| 1 |
-----
| 2 |
-----
| - |
-----
| - |
=====

Requesting page 3
=====
| 1 |
-----
| 2 |
-----
| 3 |
-----
| - |
=====

Requesting page 4
=====
| 1 |
-----
| 2 |
-----
| 3 |
-----
| 4 |
=====

Requesting page 1
=====
| 1 |
-----
| 2 |
-----
| 3 |
-----
| 4 |
=====

Requesting page 2
=====
| 1 |
-----
| 2 |
-----
| 3 |
-----
| 4 |
=====

Requesting page 5
=====
| 5 |
-----
| 2 |
-----
| 3 |
-----
| 4 |
=====

Requesting page 1
=====
| 5 |
-----
| 1 |
-----
| 3 |
-----
| 4 |
=====

Requesting page 2
=====
| 5 |
-----
| 1 |
-----
| 2 |
-----
| 4 |
=====

Requesting page 3
=====
| 5 |
-----
| 1 |
-----
| 2 |
-----
| 3 |
=====

Requesting page 4
=====
| 4 |
-----
| 1 |
-----
| 2 |
-----
| 3 |
=====

Requesting page 5
=====
| 4 |
-----
| 5 |
-----
| 2 |
-----
| 3 |
=====


Total interrupts: 10

```
