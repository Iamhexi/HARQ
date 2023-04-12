# HARQ
The transmission channel C# implementation with Hybrid Automatic Repeat Request (HARQ) error-control methods.

## Packet
### Packer header
Any header consists of:
- 32 bits for id
- 32 bits for source SA (Simulation Address - counterpart of IP) [constant]
- 32 bits for destination SA [constant]

For instance:
0000 0000 0000 0000 0000 0000 0001 0110 => id = 22
0000 0000 0000 0000 0000 0000 0000 0000 => sourceSA = 0.0.0.0
1000 0000 0000 0000 0000 0000 0000 0000 => destinationSA = 128.0.0.0