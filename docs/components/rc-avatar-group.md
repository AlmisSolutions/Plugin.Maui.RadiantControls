# RcAvatarGroup

The RcAvatarGroup is a component that represents a group of [RcAvatar](./rc-avatar.md#rcavatar) displayed in a stack.

## Properties
| Property Name            | Description                                      | Type                            |
| ------------------------ | ------------------------------------------------ | ------------------------------- |
| Avatars                | Avatars collection.            | ObservableCollection<[RcAvatar](./rc-avatar.md#rcavatar)> |
| StackDirection         | Stack direction of the avatar group.               | [StackDirection](#stackdirection)                |

### StackDirection

Represents the direction in which avatars are stacked.

- `BottomToTop`: Avatars are stacked from bottom to top.
- `TopToBottom`: Avatars are stacked from top to bottom.
