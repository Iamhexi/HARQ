# HARQ
The transmission channel C# implementation with Hybrid Automatic Repeat Request (HARQ) error-control methods.

## Commands
- `make` - builds the enitre simulation
- `make run` - runs the simulation
- `make histogram` - using the data from the recent simulation, displays a histogram of retransmissions

## TODO
- [x] implement the sender-to-receiver trasmission framework with the BSC
- [x] the same as above but with the Gilbert-Elliot model
- [x] define the packet headers
- [x] implement the packet headers
- [x] prepare the data for a histogram including number of packets sent, number of retransmissions happened once, twice, thrice and so on
- [x] implement the `make histogram` command
- [x] implement usage of detection codes using already existing solutions, namely, CRC8 and CRC32
- [x] include false positives in statistics (reported as correct but actually incorrect)
- [x] implement an encoder and a decoder for the Reed-Solomon correction code

## Dependencies
- make - build the project and run the commands
- Octave - to plot histograms
- mono (or other C# implementation) - to run C#