# HARQ
The transmission channel C# implementation with Hybrid Automatic Repeat Request (HARQ) error-control methods.

## TODO
- [x] implement the sender-to-receiver trasmission framework with the BSC and the Gilbert-Elliot model
- [x] define the packet headers
- [x] implement the packet headers
- [ ] prepare the data for a histogram including number of packets sent, number of retransmissions happened once, twice, thrice and so on
- [x] implement usage of detection codes using already existing solutions, namely, CRC8 and CRC32
- [ ] implement an encoder and a decoder for the Reed-Solomon correction code