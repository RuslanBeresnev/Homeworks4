module ILazy

/// The interface of lazy calculations
type ILazy<'a> =
    /// Get result of the calculation
    abstract member Get: unit -> 'a