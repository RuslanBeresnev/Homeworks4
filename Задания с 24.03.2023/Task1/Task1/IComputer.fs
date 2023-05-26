namespace LocalNetwork

/// Interface for computer in the network
type IComputer =

    /// Operating system of computer
    abstract member OS: string

    /// Whether computer is infected or not
    abstract member IsInfected: bool

    /// Try to infect yourself
    abstract member getInfected: unit -> unit