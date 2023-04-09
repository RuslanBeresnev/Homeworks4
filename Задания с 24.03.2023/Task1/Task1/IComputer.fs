namespace LocalNetwork

/// Operating system that sets infection probability for computer
type IOS =
    /// Infection probability
    abstract member InfectionProbability: float

/// Interface for computer in the network
type IComputer =

    /// Operating system of computer
    abstract member OS: IOS

    /// Whether computer is infected or not
    abstract member IsInfected: bool

    /// Try to infect yourself
    abstract member getInfected: unit -> unit