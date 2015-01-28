﻿// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Reflection.Metadata.Decoding
{
    public interface ICustomAttributeTypeProvider<TType> : ISignatureTypeProvider<TType>
    {
        // TODO: Review names here. SystemType isn't obviously System DOT Type. It sounds like any type of the "system".

        /// <summary>
        /// Gets the TType representation for <see cref="System.Type"/>.
        /// </summary>
        TType GetSystemType();

        /// <summary>
        /// Returns true if the given type represents <see cref="System.Type"/>.
        /// </summary>
        bool IsSystemType(TType type);

        TType GetTypeFromSerializedName(string name);

        PrimitiveTypeCode GetUnderlyingEnumType(TType type);
    }
}
