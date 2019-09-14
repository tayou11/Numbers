/*
Written by Peter O. in 2014.
Any copyright is dedicated to the Public Domain.
http://creativecommons.org/publicdomain/zero/1.0/
If you like this, you should donate to Peter O.
at: http://peteroupc.github.io/
 */
using System;

namespace PeterO.Numbers {
    /// <summary>Exception thrown for arithmetic trap errors. (The "E"
    /// stands for "extended", and has this prefix to group it with the
    /// other classes common to this library, particularly EDecimal,
    /// EFloat, and ERational.).</summary>
#if NET20 || NET40
[Serializable]
#endif
public sealed class ETrapException : ArithmeticException {
    private readonly Object result;
    private readonly EContext ctx;

    /// <summary>Gets the arithmetic context used during the operation that
    /// triggered the trap. May be null.</summary>
    /// <value>The arithmetic context used during the operation that
    /// triggered the trap. May be null.</value>
    public EContext Context {
      get {
        return this.ctx;
      }
    }

    private readonly int error;

  /// <summary>Initializes a new instance of the
  /// <see cref='PeterO.Numbers.ETrapException'/> class.</summary>
  public ETrapException() : this(FlagToMessage(EContext.FlagInvalid)) {
      }

  /// <summary>Initializes a new instance of the
  /// <see cref='PeterO.Numbers.ETrapException'/> class.</summary>
  /// <param name='message'>The parameter <paramref name='message'/> is a
  /// text string.</param>
  public ETrapException(string message) : base(message) {
         this.error = EContext.FlagInvalid;
         this.ctx = null;
         this.result = null;
      }

  /// <summary>Initializes a new instance of the
  /// <see cref='PeterO.Numbers.ETrapException'/> class.</summary>
  /// <param name='message'>The parameter <paramref name='message'/> is a
  /// text string.</param>
  /// <param name='innerException'>The parameter <paramref
  /// name='innerException'/> is an Exception object.</param>
  public ETrapException(string message, Exception innerException)
        : base(message, innerException) {
         this.error = EContext.FlagInvalid;
         this.ctx = (this.ctx == null) ? null : this.ctx.Copy();
         this.result = this.result;
      }

    /// <summary>Gets the defined result of the operation that caused the
    /// trap.</summary>
    /// <value>The defined result of the operation that caused the
    /// trap.</value>
    public Object Result {
      get {
        return this.result;
      }
    }

    /// <summary>Gets the flag that specifies the kind of error
    /// (EContext.FlagXXX). This will only be one flag, such as
    /// <c>FlagInexact</c> or FlagSubnormal.</summary>
    /// <value>The flag that specifies the kind of error
    /// (EContext.FlagXXX). This will only be one flag, such as.
    /// <c>FlagInexact</c> or FlagSubnormal.</value>
    public int Error {
      get {
        return this.error;
      }
    }

    private static string FlagToMessage(int flag) {
      return (flag == EContext.FlagClamped) ? "Clamped" : ((flag ==
        EContext.FlagDivideByZero) ? "DivideByZero" : ((flag ==
        EContext.FlagInexact) ? "Inexact" : ((flag ==
        EContext.FlagInvalid) ? "Invalid" : ((flag ==
        EContext.FlagOverflow) ? "Overflow" : ((flag ==
        EContext.FlagRounded) ? "Rounded" : ((flag ==
        EContext.FlagSubnormal) ? "Subnormal" : ((flag ==
        EContext.FlagUnderflow) ? "Underflow" : "Trap")))))));
    }

    /// <summary>Initializes a new instance of the
    /// <see cref='PeterO.Numbers.ETrapException'/>.</summary>
    /// <param name='flag'>The flag that specifies the kind of error
    /// (EContext.FlagXXX). This will only be one flag, such as
    /// <c>FlagInexact</c> or FlagSubnormal.</param>
    /// <param name='ctx'>The arithmetic context used during the operation
    /// that triggered the trap. Can be null.</param>
    /// <param name='result'>The defined result of the operation that
    /// caused the trap.</param>
    public ETrapException(int flag, EContext ctx, Object result)
      : base(FlagToMessage(flag)) {
      this.error = flag;
      this.ctx = (ctx == null) ? null : ctx.Copy();
      this.result = result;
    }

#if NET20 || NET40
    /// <summary>Initializes a new instance of the
    /// <see cref='PeterO.Cbor.ETrapException'/> class. Uses the given
    /// serialization and streaming contexts.</summary>
    /// <param name='info'>A System.Runtime.Serialization.SerializationInfo
    /// object.</param>
    /// <param name='context'>A
    /// System.Runtime.Serialization.StreamingContext object.</param>
      protected ETrapException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
        : base(info, context) {
 }
#endif
  }
}
