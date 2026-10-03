# 🔒 Security Policy

Thank you for helping keep **Space Invaders: C# Remake** and its users safe.

## 📦 Supported Versions

This project is under active development. Security fixes are applied only to the latest code on the default branch.

| Version                  | Supported          |
| ------------------------ | ------------------ |
| `master` (latest)        | ✅ Yes             |
| Older commits / releases | ❌ No              |

## 🐛 Reporting a Vulnerability

**Please do not report security vulnerabilities through public GitHub issues, discussions or pull requests.**

Instead, use one of the following private channels:

1.  **GitHub Private Vulnerability Reporting** (preferred): go to the **Security** tab of the repository and click **Report a vulnerability**.
2.  **Email**: send a message to `your-email@example.com` with the subject `[SECURITY] Space Invaders`.

### What to include

To help us understand and reproduce the issue quickly, please provide:

*   A clear description of the vulnerability and its potential impact.
*   Step-by-step instructions to reproduce it (or a proof of concept).
*   The affected version or commit hash.
*   Your operating system (Windows / Linux) and .NET SDK version.
*   Any suggested fix or mitigation, if you have one.

## ⏱️ What to Expect

This is a small, community-driven project, so response times are best-effort:

*   **Acknowledgment**: within about 7 days.
*   **Assessment & updates**: we will keep you informed about the progress.
*   **Fix & disclosure**: once a fix is available, we will publish it and, if you wish, credit you in the release notes.

Please give us a reasonable amount of time to fix the issue before any public disclosure.

## 🎯 Scope

Examples of issues we consider in scope:

*   Vulnerabilities in how the game loads or handles files (assets, configuration, save data).
*   Unsafe handling of external input that could lead to crashes or code execution.
*   Vulnerable or malicious third-party dependencies (NuGet packages).
*   Supply-chain concerns in build scripts or CI workflows.

Out of scope: gameplay bugs, cheating or exploits that only affect a local single-player session, and issues in third-party libraries that should be reported to their own maintainers (e.g. [Raylib-cs](https://github.com/raylib-cs/raylib-cs)).

## 🛡️ Security Best Practices for Users

*   Download releases only from the official repository.
*   Keep your .NET SDK/runtime up to date.
*   Do not run executables from untrusted sources claiming to be this project.

> This project is provided under the [MIT License](LICENSE), without any warranty. There is no bug bounty program.