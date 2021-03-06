﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace System.IO.IsolatedStorage
{
    public class DeleteDirectoryTests : IsoStorageTest
    {
        [Fact]
        public void DeleteDirectory_ThrowsArugmentNull()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                Assert.Throws<ArgumentNullException>(() => isf.DeleteDirectory(null));
            }
        }

        [Fact]
        public void DeleteDirectory_ThrowsObjectDisposed()
        {
            IsolatedStorageFile isf;
            using (isf = IsolatedStorageFile.GetUserStoreForAssembly())
            {
            }

            Assert.Throws<ObjectDisposedException>(() => isf.DeleteDirectory("foo"));
        }

        [Fact]
        public void DeleteDirectory_ThrowsIsolatedStorageException()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                isf.Remove();
                Assert.Throws<IsolatedStorageException>(() => isf.DeleteDirectory("foo"));
            }
        }

        [Fact]
        public void DeleteDirectory_ThrowsInvalidOperationException()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                isf.Close();
                Assert.Throws<InvalidOperationException>(() => isf.DeleteDirectory("foo"));
            }
        }

        [Fact]
        public void DeleteDirectory_RaisesInvalidPath()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                Assert.Throws<IsolatedStorageException>(() => isf.DeleteDirectory("\0bad"));
            }
        }

        [Fact]
        public void DeleteDirectory_DeleteNested()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                string directory = "DeleteDirectory_DeleteNested";
                string subdirectory = Path.Combine(directory, "Subdirectory");
                isf.CreateDirectory(subdirectory);
                Assert.True(isf.DirectoryExists(subdirectory));

                // Shouldn't be recursive
                Assert.Throws<IsolatedStorageException>(() => isf.DeleteDirectory(directory));

                isf.DeleteDirectory(subdirectory);
                isf.DeleteDirectory(directory);
                Assert.False(isf.DirectoryExists(directory));
            }
        }

        [Theory MemberData(nameof(ValidStores))]
        public void DeleteDirectory_DeletesDirectory(PresetScopes scope)
        {
            using (var isf = GetPresetScope(scope))
            {
                isf.CreateDirectory("DeleteDirectory_DeletesDirectory");
                Assert.True(isf.DirectoryExists("DeleteDirectory_DeletesDirectory"));
                isf.DeleteDirectory("DeleteDirectory_DeletesDirectory");
                Assert.False(isf.DirectoryExists("DeleteDirectory_DeletesDirectory"));
            }
        }
    }
}
